package testCMD;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.util.LinkedList;
import java.util.List;

public class LoadProperties {
	
	private String path;
	private String apkpath;
	private String testapkpath;
	private String testCaseFile;
	private List<String> targetlists = new LinkedList<String>();
	private List<String> maillists = new LinkedList<String>();
	private String simulator;
	
	public String getSimulator() {
		return simulator;
	}

	public void setSimulator(String simulator) {
		this.simulator = simulator;
	}

	public LoadProperties(String path)
	{
		this.path = path;
	}
	
	public void readProperties()
	{
		File file = new File(this.path);
		BufferedReader reader= null;
		
		try{
			reader = new BufferedReader(new FileReader(file));
			String tempString = null;
			
			while ((tempString = reader.readLine()) != null)
			{
				if (tempString.contains("#"))
				{
					continue;
				}
				else if (tempString.contains("target"))
				{
					String[] target = tempString.split("=");
					String[] target1 = target[1].split(",");
					
					for (int i = 0; i < target1.length; i ++)
					{
						System.out.println(target1[i]);
						this.targetlists.add(target1[i]);
					}
				}
				else if (tempString.contains("apkpath"))
				{
					String[] apkpath = tempString.split("=");
					System.out.println(apkpath[1]);
					this.setApkPath(apkpath[1]);
				}
				else if (tempString.contains("testapk"))
				{
					String[] testapkpath = tempString.split("=");
					System.out.println(testapkpath[1]);
					this.setTestapkpath(testapkpath[1]);
				}
				else if (tempString.contains("package"))
				{
					String[] package1 = tempString.split("=");
					System.out.println(package1[1]);
				}
				else if (tempString.contains("maillists"))
				{
					String[] maillists = tempString.split("=");
					String[] mail = maillists[1].split(",");
					
					for (int i = 0; i < mail.length; i ++)
					{
						System.out.println(mail[i]);
						this.maillists.add(mail[i]);
					}
				}
				else if (tempString.contains("testCaseFile"))
				{
					String[] testCaseFile = tempString.split("=");
					System.out.println(testCaseFile[1]);
					this.setTestCaseFile(testCaseFile[1]);
				}
				else if (tempString.contains("simulator"))
				{
					String[] testSimulator = tempString.split("=");
					System.out.println(testSimulator[1]);
					this.setSimulator(testSimulator[1]);
				}
			}
			reader.close();
		}
		catch(IOException e)
		{
			e.printStackTrace();
		}
		finally
		{
			if (reader != null)
			{
				try
				{
					reader.close();
				}
				catch(IOException e1)
				{
					e1.printStackTrace();
				}
			}
		}
	}

	public void createFileDir()
	{
		File dirFile  = null ;
        try {
           dirFile  =   new  File("C:/tmp");
            if ( ! (dirFile.exists())  &&   ! (dirFile.isDirectory())) {
                boolean  creadok  =  dirFile.mkdirs();
                if (creadok) {
                   System.out.println( " ok:创建文件夹成功！ " );
               } else {
                   System.out.println( " err:创建文件夹失败！ " );                    
               } 
           } 
        } catch (Exception e) {
           e.printStackTrace();
           System.out.println(e);
       } 
	}
	
	public  void copyBatFile()
	{
		InputStream is = TestCMD.class.getResourceAsStream("/testCMD/test.bat");
		
		try
		{
			FileOutputStream output = new FileOutputStream("C:/tmp/test.bat");
			BufferedInputStream bis = new BufferedInputStream(is);
			BufferedOutputStream bos = new BufferedOutputStream(output);

			byte[] byt = new byte[1024];

			while (bis.read(byt) != -1) {

				bos.write(byt, 0, byt.length);

				bos.flush();
			}

			try {			
				is.close();
				output.close();
				bis.close();
				bos.close();

			} catch (Exception e) {
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	public  void copyKeystoreFile()
	{
		InputStream is = TestCMD.class.getResourceAsStream("/testCMD/debug.keystore");
		
		try
		{
			FileOutputStream output = new FileOutputStream("C:/tmp/debug.keystore");
			BufferedInputStream bis = new BufferedInputStream(is);
			BufferedOutputStream bos = new BufferedOutputStream(output);

			byte[] byt = new byte[1024];

			while (bis.read(byt) != -1) {

				bos.write(byt, 0, byt.length);

				bos.flush();
			}

			try {			
				is.close();
				output.close();
				bis.close();
				bos.close();

			} catch (Exception e) {
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	public void setApkPath(String apkpath)
	{
		this.apkpath = apkpath;
	}
	
	public String getApkPath()
	{
		return this.apkpath;
	}
	
	public void setTestCaseFile(String testCaseFile)
	{
		this.testCaseFile = testCaseFile;
	}
	
	public String getTestCaseFile()
	{
		return this.testCaseFile;
	}
	
	public void setTestapkpath(String testapkpath)
	{
		this.testapkpath = testapkpath;
	}
	
	public String getTestapkpath()
	{
		return this.testapkpath;
	}
	
	public List<String> getTargetlists()
	{
		return this.targetlists;
	}
	
	public List<String> getMaillists()
	{
		return this.maillists;
	}
}
