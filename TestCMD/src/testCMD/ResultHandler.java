package testCMD;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.InputStreamReader;
import java.io.PrintWriter;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.w3c.dom.Document;
import org.w3c.dom.Element;

public class ResultHandler {
	
	private Document document;
	private String path;
	
	public ResultHandler(String path)
	{
		try
		{
			DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
			DocumentBuilder builder = factory.newDocumentBuilder();
			this.document = builder.newDocument();
		}
		catch (ParserConfigurationException e)
		{
			e.printStackTrace();
		}
		
		this.path = path;
	}

	public void createXML()
	{
		Element root = this.document.createElement("testsuites");
		this.document.appendChild(root);
		
		TransformerFactory tf = TransformerFactory.newInstance();
		
		try{
			Transformer transformer = tf.newTransformer();
			DOMSource source = new DOMSource(document);
			transformer.setOutputProperty(OutputKeys.ENCODING, "gb2312");
			transformer.setOutputProperty(OutputKeys.INDENT, "yes");
			PrintWriter pw = new PrintWriter(new FileOutputStream("h:\\result.xml"));
			StreamResult result = new StreamResult(pw);
			transformer.transform(source, result);
		}
		catch(TransformerConfigurationException e)
		{
			e.printStackTrace();
		}
		catch(FileNotFoundException e)
		{
			e.printStackTrace();
		}
		catch(TransformerException e)
		{
			e.printStackTrace();
		}
	}
	
	public void createFile()
	{
		try
		{
			File file = new File("e:/result.xml");
	        if(!file.exists())
	            if(!file.createNewFile())
	                throw new Exception("文件不存在，创建失败！");
	        
	        String s = new String();
		    String s1 = new String();
		    
			BufferedReader input = new BufferedReader(new FileReader(file));
			
			while ((s = input.readLine()) != null)
			{
				s1 += s + "\n";
			}
			
			input.close();
			s1 += "<?xml version='1.0' encoding='utf-8' standalone='yes' ?><testsuites></testsuites>";
			
			BufferedWriter output = new BufferedWriter(new FileWriter(file));
			output.write(s1);
			output.close();
		    
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
		
	}

	public void loadFile(String path)
	{
		String s = null;
		StringBuffer sb = new StringBuffer();
		File f = new File(path);
		
		try{
			BufferedReader reader = new BufferedReader(new InputStreamReader(new FileInputStream(f)));
			
			while ((s = reader.readLine()) != null)
			{
				sb.append(s);
			}
			
			String[] sb1 = sb.toString().split("<testsuites>");
			
			String[] sb2 = sb1[1].split("</testsuites>");
			
			this.writeFile(this.path, sb2[0]);
			
			reader.close();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
	
	public void writeFile(String path, String content)
	{
		File file = new File(path);
        if(!file.exists())
        	System.out.println("文件不存在，写入失败");
        
		String s = new String();
	    String s1 = new String();
	    
	    try
	    {
	    	BufferedReader input = new BufferedReader(new FileReader(file));
			
			while ((s = input.readLine()) != null)
			{
				s1 += s + "\n";
			}
			
			input.close();
			s1.replace("<testsuites>", "<testsuites>" + content);
			BufferedWriter output = new BufferedWriter(new FileWriter(file));
			output.write(s1.replace("<testsuites>", "<testsuites>" + content));
			output.close();
	    }
	    catch(Exception e)
	    {
	    	e.printStackTrace();
	    }
	}
}
