# AutoPuTTY
is a simple connection manager / launcher
It's written in C# so you'll need some Microsoft .NET Framework

Site : http://r4di.us/autoputty/
Source : https://github.com/r4dius/AutoPuTTY

## What you can do with it ##
- Manage a server list and connect thru PuTTY, WinSCP, Microsoft Remote Desktop and VNC (only VNC 3.3 encryption is supported for passwords yet)
- Easily connect to multiple servers at once
- Import a list from a simple text file
- Protect the application startup with a password (note that the list is always encrypted)

## It looks complicated isn't it ? ##
- No, it's not

## So how do I use this ? ##

### Configure tools ###
- Click the "wheel" button and select paths for the tools you want to use, default configuration will run tools from current path (except for RDP which is : %WINDIR%\system32\mstsc.exe) 

### Adding a server ###
- Name and Hostname are required, Username and Password are optional
- Name is unique
- Don't forget the connection "Type", default is PuTTY
- You can import server lists from the Options / General menu ("wheel" button)
- SSH jump can be used by adding proxy syntax before the username, it looks like :
  > proxy_username **@** proxy_host_or_ip **:** proxy_port **#** username

  ":proxy_port" is optional, will use port 22 as default

### Connect to a server ###
- Double clicking a server will connect using the associated "Type"
- Right click on a server or a multiple server selection in the list, if you choose "Connect" each will use their associated "Type"

### Modifying a server ###
- Change any of the informations and click the blue "left arrow" button

### Duplicating a server ###
- Change the Name and click the green "plus" button

### Delete a server ###
- Select a server and either click the red "cross" button or right click the server in the list and select "Delete"
- To delete multiple servers, select them then right click on the selection and select "Delete"

### Search servers ###
- Press Ctrl+F, if window size is large enougth, there's a "Match case" option too

## Source includes ##
- A modified DPAPI crypto class (http://www.obviex.com/samples/dpapi.aspx) from Chip Forster : http://www.remkoweijnen.nl/blog/2007/10/18/how-rdp-passwords-are-encrypted/#comment-562
- A VNC crypto class from VncSharp : http://cdot.senecac.on.ca/projects/vncsharp/
- The FusionTrackBar class from FusionToolkit : http://transparenttrackbar.codeplex.com/
- Code to set image opacity : http://www.geekpedia.com/code110_Set-Image-Opacity-Using-Csharp.html

#

Please report bugs or lost money to autoputty@r4di.us
