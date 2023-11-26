##################################################
Tested on Cisco Prime Infrastructure v3.1.0.0.132-1 Technet24.ir
##################################################

enable the root shell on console (through ssh)

prime/admin# su passwd root


prime/admin# shell

Enter shell access password :
Starting bash shell ...
ade #

Reset password for root

ade # sudo passwd root
Changing password for user root.
New password:
Retype new password:
passwd: all authentication tokens updated successfully.
ade #

Then login to root-shell
ade # su
Password:
ade # whoami
root

Transfer the flexlm-10.9.jar onto the pi server. I just uploaded it to a webserver, and used wget to grab it...
ade # wget http://x.x.x.x/flexlm-10.9.jar

Replace the below files with the flexlm-10.9.jar file.
ade # cp flexlm-10.9.jar /opt/CSCOlumos/staging/pf/com.cisco.xmp.osgi.flexlm-10.8-bundle.jar
ade # cp flexlm-10.9.jar /opt/CSCOlumos/staging/pf/com.cisco.xmp.osgi.flexlm-10.8.jar
ade # cp flexlm-10.9.jar /opt/CSCOlumos/lib/pf_third_party/com.cisco.xmp.osgi.flexlm-10.8-bundle.jar
ade # cp flexlm-10.9.jar /opt/CSCOlumos/lib/pf_third_party/com.cisco.xmp.osgi.flexlm-10.8.jar

Type exit, and then type reload to restart the VM... And wait...

After restart Log onto the web frontend, and go to Administration / Settings / Appliance
Copy the line after: Serial Number (Including the hostname)

In each license file replace 'nodename:loooong-serial-code' with the Serial Number you just copied and save the files.

Now go to Administration / Licenses and Software Updates / Licenses
And add all license files to PI.

PI 3 should now be licensed! Happy days :)
##################################################





























































Technet24.ir
