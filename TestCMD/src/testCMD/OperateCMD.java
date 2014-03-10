package testCMD;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

public class OperateCMD{
	
	public OperateCMD()
	{
	}
	
	public String operate(String cmd)
	{
		try {
			Process process = Runtime.getRuntime().exec(cmd);
			InputStream is2 = process.getErrorStream();
			InputStream is1 = process.getInputStream();
			
			
			BufferedReader reader_error = new BufferedReader(new InputStreamReader(is2));
			BufferedReader reader = new BufferedReader(new InputStreamReader(is1));
			
			System.out.println(2);
			String line = null;
			while((line = reader_error.readLine()) != null) 
			{
				System.out.println(line); 
			}
			
			System.out.println(1); 
			String line1 = null;
			
			while((line1 = reader.readLine()) != null)
			{
				System.out.println(line1);
				if (line1.contains("shortMsg=Process crashed"))
				{
					return "crash";
				}
			}
			reader.close();

			reader_error.close();
	
			process.waitFor();
			
			return "true";
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return "false";
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return "false";
		}
	}
	
	public void execBat(String cmd)
	{
		try {
			Process process = Runtime.getRuntime().exec("cmd /c start " + cmd);
			
			InputStream is2 = process.getErrorStream();
			InputStream is1 = process.getInputStream();
			
			
			BufferedReader reader_error = new BufferedReader(new InputStreamReader(is2));
			BufferedReader reader = new BufferedReader(new InputStreamReader(is1));
			
			System.out.println(2);
			String line = null;
			while((line = reader_error.readLine()) != null) 
			{
				System.out.println(line); 
			}
			
			System.out.println(1); 
			String line1 = null;
			
			while((line1 = reader.readLine()) != null)
			{
				System.out.println(line1);
			}
			reader.close();

			reader_error.close();
	
			process.waitFor();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
