package testCMD;
import java.util.Iterator;
import java.util.List;


public class Autotest {

	public Autotest()
	{
		
	}
	
	public void run(String properties_path)
	{
		try {
			 LoadProperties properties = new LoadProperties(properties_path);
			 properties.createFileDir();
			 properties.copyBatFile();
			 properties.copyKeystoreFile();
		     properties.readProperties();
			 String[] apkname = properties.getApkPath().split("\\\\");
			 String[] testapkname = properties.getTestapkpath().split("\\\\");
			
			 OperateCMD operate = new OperateCMD();
			 //operate.operate("echo no | android create avd --name OrangeAutoTest --target android-10 --force --abi x86");
		     if (Boolean.parseBoolean(properties.getSimulator()))
		     {
		    	 String command = "cmd /c start cmd.exe /K emulator -avd OrangeAutoTest";
			     Runtime.getRuntime().exec(command);
		     }
		     
		     operate.execBat("winrar d "+ properties.getApkPath() +" meta-inf");
		     operate.execBat("winrar d "+ properties.getTestapkpath() +" meta-inf");
			 operate.execBat("C:/tmp/test.bat " + properties.getApkPath() + " C:/tmp/debug_" + apkname[2] + " C:/tmp/debug.keystore");
			 operate.execBat("C:/tmp/test.bat " + properties.getTestapkpath() + " C:/tmp/debug_" + testapkname[2] + " C:/tmp/debug.keystore");
		     
		     operate.operate("adb wait-for-device install -r C:/tmp/debug_" + apkname[2]);
		     operate.operate("adb wait-for-device install -r C:/tmp/debug_" + testapkname[2]);
			 
			 ParseXML xml = new ParseXML();
			 xml.loadXML(properties.getTestCaseFile());
			 ResultHandler resultXml = new ResultHandler("E:/result.xml");
			 resultXml.createFile();
			 String path = "E:\\testcase.xml";
			
			 List<String> method = xml.getIncludeName();
			
			 Iterator<String> i = method.iterator();
			 int crash_count = 0;
			
			 while (i.hasNext())
			 {
				 String tmp = (String)i.next();
				 
				 String result = operate.operate("adb shell am instrument -e class " + xml.getClassName() + "#"
				 		+ tmp +" -w " + xml.getPackageName() + "/com.zutubi.android.junitreport." +
				 		"JUnitReportTestRunner");
				 
				 while (result == "crash" && crash_count < 3)
				 {
					System.out.println("ASDADADASDASDASDCrush");
					result = operate.operate("adb shell am instrument -e class " + xml.getClassName() + "#"
						 		+ tmp +" -w " + xml.getPackageName() + "/com.zutubi.android.junitreport." +
						 		"JUnitReportTestRunner");
					crash_count++;
				 }
				 
				 if (crash_count == 3)
				 {
					 resultXml.writeFile(path, result);
				 }
				 System.out.println("ASDADADASDASDASDCrush");
				 operate.operate("adb pull /data/data/com.tiancity.tsi/files/junit-report.xml " + path);
				 resultXml.loadFile(path);
			 }

		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}
