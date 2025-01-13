# FastGPUP
A WIP GUI app to make GPU-P way easier! </br>
Based on : https://github.com/jamesstringerparsec/Easy-GPU-PV
# Usage
The usage is simple, choose VM, choose GPU, slide the percentage you want to allocate of it and then press Add.</br>
For drivers you press Mount VHD, when done it will open disk management where you'll assign a letter for the mounted drive (if it doesn't already has one) and then you choose that drive letter on the tool and press "install/update driver" button.</br>
</br>
Windows 10 does not allow GPU selection, this is an Hyper-V limitation.</br>
This tool requires .NET Framework 6.0.</br>

# To-do
-Detect mismatched guest GPU drivers</br>
-Bulk GPU driver install</br>
-Install additional addons that are usually needed inside VMs (example: dummy video adapter)</br>
-AMD GPU support</br>
-Intel Arc GPU Support</br>