# AutoPuTTY
is a simple connection manager / launcher
It's written in C# so you'll need some Microsoft .NET Framework

Site : http://r4di.us/autoputty/
Source : https://github.com/r4dius/AutoPuTTY

## What you can do with it ##
- Manage a server list and connect thru PuTTY, WinSCP, Microsoft Remote Desktop and VNC (only VNC 3.3 encryption is supported for passwords yet)
- Easily connect to multiple servers at once
- Import a list from a simple text file
- Protect application startup with a password (note: the list is always encrypted)

## So how do I use this ? ##

### Configure tools ###
- Click the Options button (the "three dots") at the bottom right and configure paths for the tools you want to use
- By default, tools will run from current path, except for RDP which uses: %WINDIR%\system32\mstsc.exe

### Adding a server ###
- "Name" and "Hostname" are required. "Username", "Password" and "Private key" are optional
- "Name" must be unique
- Don't forget to set the connection "Type", the default is PuTTY
- You can import server lists from the Options (the "three dots" button) -> General
- SSH jump (proxy) is supported by using this syntax before the username:\
  `proxy_username`:`proxy_pass`@`proxy_host_or_ip`:`proxy_port`#`username`\
  :`proxy_pass` and :`proxy_port` are optional, will use port 22 as default

### Add and use a vault password ###
- Click the yellow "key" button to access your vault
- "Vault name" is required and must be unique. "Password" and "Private key" are optional
- Click the yellow "arrow" button to return to the server list. Then, click the "Password" text to switch to "Vault" and select the vault to use

### Connect to a server ###
- Double-click a server to connect using its associated "Type"
- Right-click a server (or multiple selected servers) and choose "Connect" each will use their configured connection "Type"

### Modifying a server or vault ###
- Edit any of the fields and click the blue "checkmark" to save changes

### Duplicating a server or vault ###
- Change the "Name" and click the green "plus" to create a duplicate

### Delete a server or vault ###
- Select a server or vault, then either click the red "cross" button or right click it and choose "Delete..."
- To delete multiple servers or vaults, select them all, right click on the selection, and choose "Delete..."

### Search servers ###
- Right-click the server list and select "Search...", or press Ctrl+F
- If the window is wide enough, you'll also see a "Match case" option

### Lock ###
- Right-click the server list and choose "Lock", or press Ctrl+L
- You'll need to re-enter your password to unlock

## Source includes ##
- A modified DPAPI crypto class (http://www.obviex.com/samples/dpapi.aspx) from Chip Forster : http://www.remkoweijnen.nl/blog/2007/10/18/how-rdp-passwords-are-encrypted/#comment-562
- A VNC crypto class from VncSharp : http://cdot.senecac.on.ca/projects/vncsharp/
- The FusionTrackBar class from FusionToolkit : http://transparenttrackbar.codeplex.com/
- Code to set image opacity : http://www.geekpedia.com/code110_Set-Image-Opacity-Using-Csharp.html
- Some stuff from ChatGPT, why not

#

Please report bugs or lost money to https://github.com/r4dius/AutoPuTTY/issues
