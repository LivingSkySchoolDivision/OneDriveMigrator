@ECHO OFF
goto donewait

:restartscript
echo %DATE% %TIME%: Waiting for system to be ready...
timeout /t 600
:donewait

:zcheck
echo %DATE% %TIME%: Checking for Z drive...
if not exist "z:\" (		
	echo %DATE% %TIME%: This user does not appear to have a Z drive!
	exit /b 666
)

:onecheck
if not exist "c:\Users\%username%\Onedrive - Living Sky School Division" (
	echo %DATE% %TIME%: OneDrive does not appear to be set up for the logged in user.	
	goto restartscript
)

:filecopy
echo %DATE% %TIME%: Copying files - this may take some time...
robocopy z:\ "c:\Users\%username%\Onedrive - Living Sky School Division" /e /XA:SH >> NUL
echo %DATE% %TIME%: Copy complete

exit /b 0