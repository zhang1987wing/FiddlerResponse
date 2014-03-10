package testCMD;

import java.io.FileInputStream;
import java.io.InputStream;
import java.util.LinkedList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;


public class ParseXML {
	
	private String packageName;
	private String className;
	private List<String> includeName = new LinkedList<String>();
	
	public void loadXML(String file)
	{
		try
		{
			DocumentBuilder  domBuilder  = DocumentBuilderFactory.newInstance().newDocumentBuilder();
			InputStream input = new FileInputStream(file);
			Document doc = (Document) domBuilder.parse(input);
			Element root = (Element) doc.getDocumentElement();
	        NodeList packages_name_node = ((Node) root).getChildNodes();

	        	for (int i = 0, size = packages_name_node.getLength(); i < size; i++)
	        	{
	        		Node package_name_node = packages_name_node.item(i);
	        		if (package_name_node.getNodeType() == Node.ELEMENT_NODE)
	        		{
	        			String package_name = package_name_node.getAttributes().getNamedItem("packageName").getNodeValue();
	        			System.out.println(package_name);
	        			this.setPackageName(package_name); 
	        		}
	        		
	        	}
	        Node node = (Node) doc.getElementsByTagName("class").item(0);
	        System.out.println(node.getAttributes().getNamedItem("name").getNodeValue());
	        this.setClassName(node.getAttributes().getNamedItem("name").getNodeValue());
	        
	        
	        NodeList methodList = doc.getElementsByTagName("include");
	        
	        for (int i = 0; i < methodList.getLength(); i ++)
	        {
	        	Node include_node = (Node) methodList.item(i);
	        	System.out.println(include_node.getAttributes().getNamedItem("name").getNodeValue());
	        	this.includeName.add(include_node.getAttributes().getNamedItem("name").getNodeValue());
	        }
		}
		catch(Exception e)
		{
			 e.printStackTrace();
		}
	}
	
	public void setPackageName(String packageName)
	{
		this.packageName = packageName;
	}
	
	public String getPackageName()
	{
		return this.packageName;
	}
	
	public void setClassName(String className)
	{
		this.className = className;
	}
	
	public String getClassName()
	{
		return this.className;
	}
	
	public List<String> getIncludeName()
	{
		return this.includeName;
	}
}
