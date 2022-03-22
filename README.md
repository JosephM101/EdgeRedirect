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
