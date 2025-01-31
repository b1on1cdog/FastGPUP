# FastGPUP
A WIP GUI app to make GPU-P way easier! </br>
Based on : https://github.com/jamesstringerparsec/Easy-GPU-PV
# Usage
The usage is simple, choose VM, choose GPU, slide the percentage you want to allocate of it and then press Add.</br>
Drivers should be installed automatically when adding a GPU partition, if you update host drivers you must press "Update driver" for GPU to work in guest.</br>
</br>
Windows 10 does not allow GPU selection, this is an Hyper-V limitation.</br>
This tool requires .NET Framework 6.0.</br>

# To-do
-Detect mismatched guest GPU drivers</br>
-Bulk GPU driver install</br>
-Install additional addons that are usually needed inside VMs (example: dummy video adapter)</br>
-Solve issue where app is unable to remove GPU partitions if GPU is no longer installed</br>