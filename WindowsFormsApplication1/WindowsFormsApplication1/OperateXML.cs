using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WindowsFormsApplication1
{
    class OperateXML
    {
        string myXMLFilePath;
        XmlDocument myXmlDoc;

        public OperateXML(string myXMLFilePath, XmlDocument myXmlDoc)
        {
            this.myXMLFilePath = myXMLFilePath;
            this.myXmlDoc = myXmlDoc;
        }

        public void GenerateXMLFile()
        {
            XmlElement rootElement = this.myXmlDoc.CreateElement("FlashResponse");
            myXmlDoc.AppendChild(rootElement);
            
            this.createGroupbox1(this.myXmlDoc, rootElement);
            this.createGroupbox2(this.myXmlDoc, rootElement);
            this.createJson_groupbox(this.myXmlDoc, rootElement);
            this.createGroupbox3(this.myXmlDoc, rootElement);
            this.createSign_groupbox(this.myXmlDoc, rootElement);
            this.createGroupbox4(this.myXmlDoc, rootElement);

            myXmlDoc.Save(this.myXMLFilePath);
        }

        public string loadXmlFile()
        {
            myXmlDoc.Load(this.myXMLFilePath);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("FlashResponse");

            XmlNodeList xnl = rootNode.ChildNodes;
            string aa = "";

            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute("id").ToString() == "1")
                {
                    XmlNode groupbox1_xn = xe.ChildNodes[0];
                    xe = (XmlElement)groupbox1_xn;
                    aa = xe.GetAttribute("value");
                }
                else if (xe.GetAttribute("id").ToString() == "2")
                {
                    XmlNode groupbox2_xn = xe.ChildNodes[0];
                    xe = (XmlElement)groupbox2_xn;
                    aa = xe.GetAttribute("value");

                    groupbox2_xn = xe.ChildNodes[1];
                    xe = (XmlElement)groupbox2_xn;
                    aa = xe.GetAttribute("value");
                }
                else if (xe.GetAttribute("id").ToString() == "json_groupbox")
                {

                }
                else if (xe.GetAttribute("id").ToString() == "3")
                {

                }
                else if (xe.GetAttribute("id").ToString() == "sign_groupbox")
                {

                }
                else if (xe.GetAttribute("id").ToString() == "4")
                {

                }
                //XmlNodeList xnl0 = xe.ChildNodes;
                //bookModel.BookName = xnl0.Item(0).InnerText;
                //bookModel.BookAuthor = xnl0.Item(1).InnerText;
                //bookModel.BookPrice = Convert.ToDouble(xnl0.Item(2).InnerText);
            }

            return aa;
        }

        private void createGroupbox1(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox1LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox1LevelElement.SetAttribute("id", "1");
            rootElement.AppendChild(groupbox1LevelElement);

            XmlElement groupbox1_secondLevelElement_chebox2 = myXmlDoc.CreateElement("checkbox2");
            groupbox1_secondLevelElement_chebox2.SetAttribute("value", "true");

            groupbox1LevelElement.AppendChild(groupbox1_secondLevelElement_chebox2);
        }

        private void createGroupbox2(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox2LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox2LevelElement.SetAttribute("id", "2");
            rootElement.AppendChild(groupbox2LevelElement);

            XmlElement groupbox2_secondLevelElement_url_tb = myXmlDoc.CreateElement("url_tb");
            groupbox2_secondLevelElement_url_tb.SetAttribute("value", "abc");
            XmlElement groupbox2_secondLevelElement_requestType_cb = myXmlDoc.CreateElement("requestType_cb");
            groupbox2_secondLevelElement_requestType_cb.SetAttribute("value", "POST");

            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_url_tb);
            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_requestType_cb);
        }

        private void createJson_groupbox(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement json_groupboxLevelElement = myXmlDoc.CreateElement("json_groupbox");
            json_groupboxLevelElement.SetAttribute("id", "json_groupbox");
            rootElement.AppendChild(json_groupboxLevelElement);

            XmlElement json_groupbox_secondLevelElement_preview_response = myXmlDoc.CreateElement("preview_response");
            json_groupbox_secondLevelElement_preview_response.SetAttribute("value", "abc");

            json_groupboxLevelElement.AppendChild(json_groupbox_secondLevelElement_preview_response);
        }

        private void createSign_groupbox(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement sign_groupboxLevelElement = myXmlDoc.CreateElement("groupbox");
            sign_groupboxLevelElement.SetAttribute("id", "sign_groupbox");
            rootElement.AppendChild(sign_groupboxLevelElement);

            XmlElement sign_groupbox_secondLevelElement_signValue_text = myXmlDoc.CreateElement("signValue_text");
            sign_groupbox_secondLevelElement_signValue_text.SetAttribute("value", "abc");

            sign_groupboxLevelElement.AppendChild(sign_groupbox_secondLevelElement_signValue_text);
        }

        private void createGroupbox3(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox3LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox3LevelElement.SetAttribute("id", "3");
            rootElement.AppendChild(groupbox3LevelElement);

            XmlElement groupbox3_secondLevelElement_para = myXmlDoc.CreateElement("Para");

            XmlElement groupbox3_thirdLevelElement_paraName = myXmlDoc.CreateElement("paraName");
            groupbox3_thirdLevelElement_paraName.InnerText = "Lenovo";
            XmlElement groupbox3_thirdLevelElement_paraValue = myXmlDoc.CreateElement("paraValue");
            groupbox3_thirdLevelElement_paraValue.InnerText = "abc";
            XmlElement groupbox3_thirdLevelElement_paraType = myXmlDoc.CreateElement("paraType");
            groupbox3_thirdLevelElement_paraType.InnerText = "请求之";
            XmlElement groupbox3_thirdLevelElement_paraDataType = myXmlDoc.CreateElement("paraDataType");
            groupbox3_thirdLevelElement_paraDataType.InnerText = "Int";

            groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraName);
            groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraValue);
            groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraType);
            groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraDataType);


            groupbox3LevelElement.AppendChild(groupbox3_secondLevelElement_para);
        }

        private void createGroupbox4(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox4LevelElement = this.myXmlDoc.CreateElement("groupBox");
            groupbox4LevelElement.SetAttribute("id", "4");
            rootElement.AppendChild(groupbox4LevelElement);

            XmlElement groupBox4_secondLevelElement_response_ta = myXmlDoc.CreateElement("response_ta");
            groupBox4_secondLevelElement_response_ta.SetAttribute("value", "abc");

            groupbox4LevelElement.AppendChild(groupBox4_secondLevelElement_response_ta);

            myXmlDoc.Save(myXMLFilePath);
        }
    }
}
