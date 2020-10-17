@echo off

rem Microsoft Ajax Minifier  http://aspnet.codeplex.com/releases/view/40584
ajaxmin -js -clobber -literals:combine -global:$,ActionDescriptor,ActionReference,app,charIDToTypeID,DescValueType,DialogModes,executeAction,executeActionGet,File,Folder,Socket,stringIDToTypeID,typeIDToCharID,typeIDToStringID,XML .\CSS3Ps\src\CSS3Ps.jsx -o .\CSS3Ps\bin-debug\CSS3Ps.jsx

mkdir .\CSS3Ps\bin-debug\manual
copy .\CSS3Ps\bin-debug\CSS3Ps.jsx .\CSS3Ps\bin-debug\manual\CSS3Ps.jsx
echo ;generate2(); >> .\CSS3Ps\bin-debug\manual\CSS3Ps.jsx
copy .\CSS3Ps\bin-debug\manual\CSS3Ps.jsx .\public\CSS3Ps.jsx

copy .\CSS3Ps\src\CSS3Ps.jsx .\CSS3Ps\bin-debug\manual\CSS3PsDebug.jsx
echo ;server='user1184.netfx45lab.discountasp.net';generate2(); >> .\CSS3Ps\bin-debug\manual\CSS3PsDebug.jsx
copy .\CSS3Ps\bin-debug\manual\CSS3PsDebug.jsx .\public\CSS3PsDebug.jsx

java -jar .\signingtoolkit\ucf.jar -package -storetype PKCS12 -keystore codesignfull.pfx -storepass !1toqekvurt -tsa https://timestamp.geotrust.com/tsa .\public\CSS3Ps.zxp -C ".\\CSS3Ps\\bin-debug" CSS3Ps.mxi CSS3Ps.jsx manual\CSS3Ps.jsx CSS3Ps.swf CSS3PsButton.png CSS3PsButtonWaiting.png CSS3PsDarkButton.png CSS3PsDarkButtonWaiting.png CSS3PsNormal.png CSS3PsRollover.png CSS3PsDarkNormal.png CSS3PsDarkRollover.png


