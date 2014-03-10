@if exist %1 start jarsigner -verbose -digestalg SHA1 -sigalg MD5withRSA -keystore %3 -storepass android -keypass android %1 androiddebugkey
@ping 127.0.0.1 -n 5 -w 1000 > nul
goto zipalign
:zipalign
zipalign -v 4 %1 %2
@taskkill /f /im cmd.exe"