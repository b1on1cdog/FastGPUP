using MetroFramework.Forms;
using System.Management.Automation;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Management;

namespace FastGPU_P
{
    public partial class Form1 : MetroForm
    {
        bool isWin10 = false;
        public Form1()
        {
            var os = Environment.OSVersion;
            isWin10 = (os.Version.Major == 10 && os.Version.Build < 22000);
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            if (isWin10)
            {
                //Win10 does not allow GPU selection
                gpuBox.Text = "Auto";
                gpuBox.Enabled = false;
            } else {
                int i = 0;

                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        gpuBox.Items.Add(obj["Name"]);
                        Console.WriteLine();
                        i++;
                    }
                }
            }

            //Only 1 GPU installed, disable selection
            if (gpuBox.Items.Count == 1) {
                    gpuBox.SelectedIndex = 0;
                    gpuBox.Enabled = false;
             }

            string _scr = ("Get-VM | Where-Object Generation -GT (1)  | Select -ExpandProperty Name");
                var _ps = PowerShell.Create();
                _ps.AddScript(_scr);
                Collection<PSObject> _cObj = _ps.Invoke();

                if (_ps.HadErrors)
                {
                    Console.WriteLine(_ps.Streams.Error[0].ToString());
                }

                foreach (PSObject _vm in _cObj)
                {
                    vmBox.Items.Add(_vm);
                }

                if (vmBox.Items.Count == 0) {
                MessageBox.Show("No VMs available!");
                Application.Exit();
                } else if (vmBox.Items.Count == 1){
                    vmBox.SelectedIndex = 0;
                    vmBox.Enabled = false;
                }

        }

        private void allocationBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.allocLabel.Text = allocationBar.Value.ToString()+"%";
        }
        private string getGPUInstance(string name)
        {
            string _scr = (@"
        $PartitionableGPUList = Get-CimInstance -Class Msvm_PartitionableGpu -Namespace root\virtualization\v2 
        $DeviceID = ((Get-CimInstance -ClassName Win32_PNPSignedDriver | where {($_.Devicename -eq " + "\""+name+"\""+ @")}).hardwareid).split('\')[1]
        $DevicePathName = ($PartitionableGPUList | Where-Object name -like *$DeviceID*).Name
        $DevicePathName | Select
");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            foreach (PSObject _id in _cObj)
            {
                return _id.ToString();
            }

            if (_ps.HadErrors)
            {
                MessageBox.Show(_ps.Streams.Error[0].ToString());
            }
            throw new Exception("instance id not found");
        }
        private void addButton_Click(object sender, EventArgs e)
        {

            //Instance path only applied if system has more than one GPU or windows 11.
            var _instacePath =  @"-InstancePath $instance";
            if (gpuBox.Items.Count == 1 || isWin10)
            {
                _instacePath = string.Empty;
            }

            string _scr = (@"
    Set-ExecutionPolicy -ExecutionPolicy Unrestricted
    $VMName = " + "\""+ vmBox.GetItemText(vmBox.Text) + "\"" +@"
    $instance = "+ "\""+ getGPUInstance(gpuBox.Text) + "\""+ @"
    [decimal]$GPUResourceAllocationPercentage = " + allocationBar.Value.ToString() + @"
    Add-VMGpuPartitionAdapter -VMName $VMName "+ _instacePath + @"
    [float]$devider = [math]::round($(100 / $GPUResourceAllocationPercentage),2)
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionVRAM ([math]::round($(1000000000 / $devider))) -MaxPartitionVRAM ([math]::round($(1000000000 / $devider))) -OptimalPartitionVRAM ([math]::round($(1000000000 / $devider)))
    Set-VMGPUPartitionAdapter -VMName $VMName -MinPartitionEncode ([math]::round($(18446744073709551615 / $devider))) -MaxPartitionEncode ([math]::round($(18446744073709551615 / $devider))) -OptimalPartitionEncode ([math]::round($(18446744073709551615 / $devider)))
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionDecode ([math]::round($(1000000000 / $devider))) -MaxPartitionDecode ([math]::round($(1000000000 / $devider))) -OptimalPartitionDecode ([math]::round($(1000000000 / $devider)))
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionCompute ([math]::round($(1000000000 / $devider))) -MaxPartitionCompute ([math]::round($(1000000000 / $devider))) -OptimalPartitionCompute ([math]::round($(1000000000 / $devider)))
    Set-VM -GuestControlledCacheTypes $true -VMName $VMName
    Set-VM -LowMemoryMappedIoSpace 1Gb -VMName $VMName
    Set-VM -HighMemoryMappedIoSpace 32GB -VMName $VMName
");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            if (_ps.HadErrors)
            {
                MessageBox.Show(_ps.Streams.Error[0].ToString());
            }
            else
            {
                MessageBox.Show("GPU Added to VM successfully!");
            }
        }

        private void installDriver_Click(object sender, EventArgs e)
        {
            //
            string _scr = @"
Set-ExecutionPolicy -ExecutionPolicy Unrestricted
Import-Module Storage
Function Add-VMGpuPartitionAdapterFiles {
param(
[string]$hostname = $ENV:COMPUTERNAME,
[string]$DriveLetter,
[string]$GPUName
)

If (!($DriveLetter -like ""*:*"")) {
    $DriveLetter = $Driveletter + "":""
    }

If ($GPUName -eq ""AUTO"") {
    $PartitionableGPUList = Get-CimInstance -Class Msvm_PartitionableGpu -Namespace root\virtualization\v2 
    $DevicePathName = $PartitionableGPUList.Name | Select-Object -First 1
    $GPU = Get-PnpDevice | Where-Object {($_.DeviceID -like ""*$($DevicePathName.Substring(8,16))*"") -and ($_.Status -eq ""OK"")} | Select-Object -First 1
    $GPUName = $GPU.Friendlyname
    $GPUServiceName = $GPU.Service 
    }
Else {
    $GPU = Get-PnpDevice | Where-Object {($_.Name -eq ""$GPUName"") -and ($_.Status -eq ""OK"")} | Select-Object -First 1
    $GPUServiceName = $GPU.Service
    }
# Get Third Party drivers used, that are not provided by Microsoft and presumably included in the OS

Write-Host ""INFO   : Finding and copying driver files for $GPUName to VM. This could take a while...""

$Drivers = Get-CimInstance -ClassName Win32_PNPSignedDriver | where {$_.DeviceName -eq ""$GPUName""}

New-Item -ItemType Directory -Path ""$DriveLetter\windows\system32\HostDriverStore"" -Force | Out-Null
#copy directory associated with sys file 
$servicePath = (Get-CimInstance -ClassName Win32_SystemDriver | Where-Object {$_.Name -eq ""$GPUServiceName""}).Pathname
                $ServiceDriverDir = $servicepath.split('\')[0..5] -join('\')
                $ServicedriverDest = (""$driveletter"" + ""\"" + $($servicepath.split('\')[1..5] -join('\'))).Replace(""DriverStore"",""HostDriverStore"")
                if (!(Test-Path $ServicedriverDest)) {
                Copy-item -path ""$ServiceDriverDir"" -Destination ""$ServicedriverDest"" -Recurse
                }

# Initialize the list of detected driver packages as an array
$DriverFolders = @()
foreach ($d in $drivers) {
    $DriverFiles = @()
    $ModifiedDeviceID = $d.DeviceID -replace ""\\"", ""\\""
    $Antecedent = ""\\"" + $hostname + ""\ROOT\cimv2:Win32_PNPSignedDriver.DeviceID=""""$ModifiedDeviceID""""""
    $DriverFiles += Get-CimInstance -ClassName Win32_PNPSignedDriverCIMDataFile | where {$_.Antecedent -eq $Antecedent}
    $DriverName = $d.DeviceName
    $DriverID = $d.DeviceID
    if ($DriverName -like ""NVIDIA*"") {
        New-Item -ItemType Directory -Path ""$driveletter\Windows\System32\drivers\Nvidia Corporation\"" -Force | Out-Null
        }
    foreach ($i in $DriverFiles) {
            $path = $i.Dependent.Split(""="")[1] -replace '\\\\', '\'
            $path2 = $path.Substring(1,$path.Length-2)
            $InfItem = Get-Item -Path $path2
            $Version = $InfItem.VersionInfo.FileVersion
            If ($path2 -like ""c:\windows\system32\driverstore\*"") {
                $DriverDir = $path2.split('\')[0..5] -join('\')
                $driverDest = (""$driveletter"" + ""\"" + $($path2.split('\')[1..5] -join('\'))).Replace(""driverstore"",""HostDriverStore"")
                if (!(Test-Path $driverDest)) {
                Copy-item -path ""$DriverDir"" -Destination ""$driverDest"" -Recurse
                }
            }
            Else {
                $ParseDestination = $path2.Replace(""c:"", ""$driveletter"")
                $Destination = $ParseDestination.Substring(0, $ParseDestination.LastIndexOf('\'))
                if (!$(Test-Path -Path $Destination)) {
                    New-Item -ItemType Directory -Path $Destination -Force | Out-Null
                    }
                Copy-Item $path2 -Destination $Destination -Force
                
            }

    }
    }

}
[string]$VMName = " + "\"" + vmBox.Text + "\"" + @"
[string]$GPUName = " + "\"" + gpuBox.Text + "\"" + @"
[string]$Hostname = $ENV:Computername
$VM = Get-VM -VMName $VMName
$VHD = Get-VHD -VMId $VM.VMId

If ($VM.state -eq ""Running"") {
    [bool]$state_was_running = $true
    }

if ($VM.state -ne ""Off""){
    ""Attemping to shutdown VM...""
    Stop-VM -Name $VMName -Force
    } 

While ($VM.State -ne ""Off"") {
    Start-Sleep -s 3
    ""Waiting for VM to shutdown - make sure there are no unsaved documents...""
    }

""Mounting Drive...""
$DriveLetter = "  + "\"" + vhdBox.Text+"\""+@"

""Copying GPU Files - this could take a while...""
Add-VMGPUPartitionAdapterFiles -hostname $Hostname -DriveLetter $DriveLetter -GPUName $GPUName

""Dismounting Drive...""
Dismount-VHD -Path $VHD.Path

If ($state_was_running){
    ""Previous State was running so starting VM...""
    Start-VM $VMName
    }

";
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            if (_ps.HadErrors)
            {
                MessageBox.Show(_ps.Streams.Error[0].ToString());
            }
            else
            {
                MessageBox.Show("GPU driver updated successfully!");
            }
        }
        private void LoadDrives()
        {

            vhdBox.Items.Clear();
            string _sdr = (@"(Get-PSDrive -PSProvider FileSystem).Root");
            var _pz = PowerShell.Create();
            _pz.AddScript(_sdr);
            Collection<PSObject> _zObj = _pz.Invoke();
            if (_pz.HadErrors)
            {
                MessageBox.Show(_pz.Streams.Error[0].ToString());
            }
            else
            {
                foreach (PSObject _driveletter in _zObj)
                {
                    string letter = _driveletter.ToString();
                    letter = letter.Replace(":\\", "");
                    if (letter != "C" && letter.Length < 3)
                    {
                        vhdBox.Items.Add(letter);
                    }

                }
            }
        }
        private void mountVHD_Click(object sender, EventArgs e)
        {
            string _scr = (@"
Import-Module Storage
Set-ExecutionPolicy -ExecutionPolicy Unrestricted
[string]$VMName = " + "\"" + vmBox.Text + "\"" + @"
$VM = Get-VM -VMName $VMName
$VHD = Get-VHD -VMId $VM.VMId
Mount-VHD -Path $VHD.Path -PassThru
");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            if (_ps.HadErrors)
            {
                MessageBox.Show(_ps.Streams.Error[0].ToString());
            }
            else
            {
                Process procDM = new Process();
                procDM.StartInfo.FileName = "C:\\Windows\\System32\\mmc.exe";
                procDM.StartInfo.Arguments = "C:\\Windows\\System32\\diskmgmt.msc";
                procDM.Start();
                MessageBox.Show("VHD Mounted Successfully!");
                LoadDrives();
            }
        }

        private void vhdBox_Click(object sender, EventArgs e)
        {
            LoadDrives();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            string _scr = ("Remove-VMGpuPartitionAdapter -VMName " +"\"" + vmBox.Text + "\"");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            _ps.Invoke();
            if (_ps.HadErrors)
            {
                Console.WriteLine(_ps.Streams.Error[0].ToString());
            }
            else {
                MessageBox.Show("successfully removed all GPU Partitions from VM");
            }
        }
    }
}

