# FastGPUP
A WIP GUI app to make GPU-P way easier!
Based on : https://github.com/jamesstringerparsec/Easy-GPU-PV
![image](https://github.com/user-attachments/assets/9b732ced-8304-4dc3-afa2-95c409702cfa)
# Usage
The usage is simple, choose VM, choose GPU, slide the percentage you want to allocate of it and then press Add. 
For drivers, choose VM, and then click install/update driver. It will mount the VM's VHD, copy the driver files, and unmount the VHD.
# Troubleshooting
If you get an error related to Execution Policy you should execute:
"Set-ExecutionPolicy -ExecutionPolicy Unrestricted" on PowerShell
