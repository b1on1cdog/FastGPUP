using MetroFramework.Forms;
using System.Management.Automation;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Management;
using Microsoft.Win32;

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
        static string GetWindowsEdition()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Caption"]?.ToString() ?? "Unknown";
                }
            }
            return "Unknown";
        }
        static bool IsWindowsCompatible()
        {
            int buildNumber = Environment.OSVersion.Version.Build;
            string edition = GetWindowsEdition();
            Debug.WriteLine("Running " + edition);
            if (buildNumber >= 19041 && !edition.Contains("Server")){
                return true;
            }
            if (buildNumber >= 20348 && edition.Contains("Server")) {
                return true;
            }
            return false;
        }
        static string GetGPUVRAM(string gpuName)
        {
            int index = 0;
            while (true) {

                string registryKeyPath = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\000"+index.ToString();
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKeyPath))
                {
                    if (key == null && index > 0)
                    {
                        return null;
                    }
                    else
                    {
                        string foundName = GetValueFromRegistry(registryKeyPath, "DriverDesc");
                        if (foundName == gpuName) {
                            return GetValueFromRegistry(registryKeyPath, "HardwareInformation.qwMemorySize");
                        }
                    }
                    index++;
                }
            }
            
        }
        static string GetValueFromRegistry(string registryKeyPath, string valueName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKeyPath))
            {
                if (key != null)
                {
                    object value = key.GetValue(valueName);
                    if (value != null)
                    {
                        return value.ToString(); 
                    }
                }
            }

            return null;
        }
        static bool FeatureEnabled(string featureName)
        {
            string query = $"SELECT * FROM Win32_OptionalFeature WHERE Name = '{featureName}'";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    int installState = Convert.ToInt32(obj["InstallState"]);
                    return installState == 1;
                }
            }

            return false;  // Feature not found or not enabled
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            if (isWin10)
            {
                //Win10 does not allow GPU selection
                gpuBox.Text = "Auto";
                gpuBox.Enabled = false;
            }
            else
            {
                int i = 0;
                int max_vram_index = 0;
                double max_vram = 0.0;

                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        gpuBox.Items.Add(obj["Name"]);
                        double vram = Convert.ToDouble(GetGPUVRAM((string)obj["Name"]));
                        if (vram > max_vram) {
                           max_vram = vram;
                           max_vram_index = i;
                        }
                        i++;

                        double GB = 1024.0 * 1024.0 * 1024.0;
                        string vram_gb = (Convert.ToDouble(GetGPUVRAM((string)obj["Name"])) / GB).ToString("0.##");
                        Debug.WriteLine(obj["Name"] + ": " + vram_gb+"GB");
                    }
                }
                gpuBox.SelectedIndex = max_vram_index;
            }

            //Only 1 GPU installed, disable selection
            if (gpuBox.Items.Count == 1)
            {
                //gpuBox.SelectedIndex = 0;
                gpuBox.Enabled = false;
            }

           //
            var _ps = PowerShell.Create();
            _ps.AddScript("Get-VM | Where-Object Generation -GT (1)  | Select -ExpandProperty Name");
            Collection<PSObject> _cObj = _ps.Invoke();

            if (_ps.HadErrors)
            {
                Debug.WriteLine(_ps.Streams.Error[0].ToString());
            }

            foreach (PSObject _vm in _cObj)
            {
                vmBox.Items.Add(_vm);
            }

            if (vmBox.Items.Count == 0)
            {
                MessageBox.Show("No VMs available!");
                Application.Exit();
            }
            else if (vmBox.Items.Count == 1)
            {
                vmBox.SelectedIndex = 0;
                vmBox.Enabled = false;
            }

            if (!IsWindowsCompatible())
            {
                DialogResult result = MessageBox.Show(
                    "This application is optimized for Windows 10 20H1 and later..\n\n" +
                    "Running on an unsupported Windows version may cause issues.\n\n" +
                    "Do you want to continue anyway?",
                    "Compatibility Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            
            if (!FeatureEnabled("Microsoft-Hyper-V-All"))
            {
                DialogResult result = MessageBox.Show(
                    "Hyper-V feature is disabled. GPU-P requires Hyper-V to be enabled.\n\n" +
                    "Do you want to continue anyway?",
                    "Hyper-V Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            
            if (FeatureEnabled("Microsoft-Windows-Subsystem-Linux"))
            {
                Debug.WriteLine("WSL is enabled, this could cause compatibility issues!");
            }

        }

        private void allocationBar_Scroll(object sender, ScrollEventArgs e)
        {
            int value = allocationBar.Value;
            value = (int)(Math.Round(allocationBar.Value / 5.0) * 5);
            allocationBar.Value = value;

            this.allocPercent.Text = allocationBar.Value.ToString() + "%";
        }
        private string getGPUInstance(string name)
        {
            string _scr = (@"
        $PartitionableGPUList = Get-CimInstance -Class Msvm_PartitionableGpu -Namespace root\virtualization\v2 
        $DeviceID = ((Get-CimInstance -ClassName Win32_PNPSignedDriver | where {($_.Devicename -eq " + "\"" + name + "\"" + @")}).hardwareid).split('\')[1]
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

        private void shutdownVM() {
            /*
             to-do:
            -implement graceful shutdown
            -store the VM running state and autostart VM after finishing execution
             */
            string _scr = (@"
    $VMName = " + "\"" + vmBox.GetItemText(vmBox.Text) + "\"" + @"
    if ((Get-VM -Name $vmName).State -eq ""Running"") {
        Stop-VM -Name $vmName -Force
    }
");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            if (_ps.HadErrors)
            {
                Console.WriteLine(_ps.Streams.Error[0].ToString());
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            shutdownVM();

            //Instance path only applied if system has more than one GPU or windows 11.
            var _instacePath = @"-InstancePath $instance";
            if (gpuBox.Items.Count == 1 || isWin10)
            {
                _instacePath = string.Empty;
            }

            string _scr = (@"
    $VMName = " + "\"" + vmBox.GetItemText(vmBox.Text) + "\"" + @"
    $instance = " + "\"" + getGPUInstance(gpuBox.Text) + "\"" + @"
    [decimal]$GPUResourceAllocationPercentage = " + allocationBar.Value.ToString() + @"
    Add-VMGpuPartitionAdapter -VMName $VMName " + _instacePath + @"
    [float]$devider = [math]::round($(100 / $GPUResourceAllocationPercentage),2)
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionVRAM ([math]::round($(1000000000 / $devider))) -MaxPartitionVRAM ([math]::round($(1000000000 / $devider))) -OptimalPartitionVRAM ([math]::round($(1000000000 / $devider)))
    Set-VMGPUPartitionAdapter -VMName $VMName -MinPartitionEncode ([math]::round($(18446744073709551615 / $devider))) -MaxPartitionEncode ([math]::round($(18446744073709551615 / $devider))) -OptimalPartitionEncode ([math]::round($(18446744073709551615 / $devider)))
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionDecode ([math]::round($(1000000000 / $devider))) -MaxPartitionDecode ([math]::round($(1000000000 / $devider))) -OptimalPartitionDecode ([math]::round($(1000000000 / $devider)))
    Set-VMGpuPartitionAdapter -VMName $VMName -MinPartitionCompute ([math]::round($(1000000000 / $devider))) -MaxPartitionCompute ([math]::round($(1000000000 / $devider))) -OptimalPartitionCompute ([math]::round($(1000000000 / $devider)))
    Set-VM -GuestControlledCacheTypes $true -VMName $VMName
    Set-VM -LowMemoryMappedIoSpace 1Gb -VMName $VMName
    Set-VM -HighMemoryMappedIoSpace 32GB -VMName $VMName
");
            /*
            string vramString = GetGPUVRAM(gpuBox.Text);
            if (vramString != null && Convert.ToDouble(vramString) > 500000000) {
                _scr = _scr.Replace("1000000000", vramString);
                _scr = _scr.Replace("18446744073709551615", vramString);
                Debug.WriteLine(_scr);
            }
            */

            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            Collection<PSObject> _cObj = _ps.Invoke();
            if (_ps.HadErrors)
            {
                MessageBox.Show(_ps.Streams.Error[0].ToString());
            }
            else
            {
                installDriver();
                MessageBox.Show("GPU partition assigned to VM!");
            }
        }

        private void installDriver()
        {
            shutdownVM();
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
$DiskNumber = (Mount-VHD -NoDriveLetter -Path $VHD.Path -PassThru | Get-Disk).Number
$PartitionNumber = (Get-Partition -DiskNumber $DiskNumber | Where-Object {$_.Type -eq ""Basic""}).PartitionNumber
$UsedLetters = Get-CimInstance -ClassName Win32_LogicalDisk | Select-Object -ExpandProperty DeviceID | ForEach-Object { $_.ToString()[0] }
$DriveLetter = [char[]](67..90) | Where-Object { $_ -notin $UsedLetters } | Select-Object -First 1
Set-Partition -DiskNumber $DiskNumber -PartitionNumber $PartitionNumber -NewDriveLetter $DriveLetter

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
                MessageBox.Show("GPU drivers updated successfully!");
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            shutdownVM();
            string _scr = ("Remove-VMGpuPartitionAdapter -VMName " + "\"" + vmBox.Text + "\"");
            var _ps = PowerShell.Create();
            _ps.AddScript(_scr);
            _ps.Invoke();
            if (_ps.HadErrors)
            {
                Console.WriteLine(_ps.Streams.Error[0].ToString());
            }
            else
            {
                MessageBox.Show("Successfully cleared all GPU Partitions from VM");
            }
        }

        private void installDriverBtn_Click(object sender, EventArgs e)
        {
            installDriver();
        }
    }
}

