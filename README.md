# EdgeRedirect

This project works to circumvent Microsoft's persistent practices to force users to use Microsoft Edge by outright replacing the original Microsoft Edge executable, and redirecting requests made to it to a browser of your choice.

I have some new names for the project in mind, such as "BleedingEdge" and "Edgy".

# Background
The initial focus on this project was with Cortana (aka. Windows Search). When you open a web link or search from Cortana, your link or search will be opened in Microsoft Edge. The original idea was that by replacing the Microsoft Edge executable file and disabling the updater, you could point Microsoft Edge requests to a different browser and/or search engine altogether.

# Building & Installing
To build the solution, you will need Visual Studio 2022 installed with the "desktop" workload.
### Building
- Open the solution in Visual Studio
- In the Solution Explorer, right-click on the solution and click "Restore NuGet Packages" to download and install the NuGet packages for the solution.
- Build the solution by clicking `Build -> Build Solution` in the menu, or by pressing <kbd>F6</kbd>.
- The solution will be built to a directory named `bin` in the solution root.
### Installing
- At the solution root, navigate to `bin`, and run `EdgeRedirect_Installer.exe` as an administrator. Follow the instructions shown to install.
### Configuring
To configure what browser to redirect requests to, go to the installation directory for Microsoft Edge (C:\Program Files (x86)\Microsoft\Edge\Application) and run `edgeredirect_config.exe` as an administrator.


# An unfortunate announcement
While debugging on Windows 11, I noticed a huge bug that broke the entire workaround.
While this workaround does work, it is rendered ineffective either after a system restart, or after the `sihost.exe` process is restarted. This issue is especially evident, for example, when attempting to open a link from Cortana. Absolutely nothing will happen. I have reason to believe that there is some sort of verification process tied to the `sihost.exe` process that checks if the Microsoft Edge executable is in fact valid.
Being a student in high school, I simply do not have the time to debug and resolve this issue. Furthermore, because I don’t know the definite cause of the issue, I don’t know what to look for or debug. I likely will not make any progress on this project for a while.

If any of you are willing to dig in and figure out what’s causing the issue, I would very much appreciate it if you submitted your findings as an issue. Anybody that does contribute to this project will be mentioned in the project credits. A huge thanks to anybody who contributes fixes/code!
