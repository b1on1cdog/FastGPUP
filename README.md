# FastGPUP
A WIP GUI app to make GPU-P way easier!
Based on : https://github.com/jamesstringerparsec/Easy-GPU-PV
# Usage
The usage is simple, choose VM, choose GPU, slide the percentage you want to allocate of it and then press Add. 
For drivers you press Mount VHD, when done it will open disk management where you'll assign a letter for the mounted drive (if it doesn't already has one) and then you choose that drive letter on the tool and press "install/update driver" button. 
# Troubleshooting
At the time of this last edit (12:02am GMT-4 16/3/2022) this wasn't tested outside my own PC, if you get an error related to Execution Policy you should execute:
"Set-ExecutionPolicy -ExecutionPolicy Unrestricted" on PowerShell
