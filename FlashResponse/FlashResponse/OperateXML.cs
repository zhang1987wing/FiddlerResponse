using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace FlashResponse
{
    class OperateXML
    {
        private string myXMLFilePath;
        private XmlDocument myXmlDoc;
        private UserTabpage oPage;
        private TextBox paraName_tb;
        private TextBox paraValue_tb;
        private ComboBox paraType_tb;
        private ComboBox paraDataType_cb;

        public OperateXML(string myXMLFilePath, XmlDocument myXmlDoc, UserTabpage oPage)
        {
            this.myXMLFilePath = myXMLFilePath;
            this.myXmlDoc = myXmlDoc;
            this.oPage = oPage;
        }

        public void GenerateXMLFile()
        {
            XmlElement rootElement = this.myXmlDoc.CreateElement("FlashResponse");
            this.myXmlDoc.AppendChild(rootElement);

            this.createGroupbox1(this.myXmlDoc, rootElement);
            this.createGroupbox2(this.myXmlDoc, rootElement);
            this.createJson_groupbox(this.myXmlDoc, rootElement);
            this.createGroupbox3(this.myXmlDoc, rootElement);
            this.createSign_groupbox(this.myXmlDoc, rootElement);
            this.createGroupbox4(this.myXmlDoc, rootElement);

            myXmlDoc.Save(this.myXMLFilePath);
        }

        public void loadXmlFile()
        {
            myXmlDoc.Load(this.myXMLFilePath);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("FlashResponse");

            XmlNodeList xnl = rootNode.ChildNodes;

            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute("id").ToString() == "1")
                {
                    XmlNode groupbox1_xn = xe.ChildNodes[0];
                    xe = (XmlElement)groupbox1_xn;
                    this.oPage.getKeyTextBox().Text = xe.GetAttribute("value");
                }
                else if (xe.GetAttribute("id").ToString() == "2")
                {
                    XmlNodeList groupbox2_xl = xn.ChildNodes;
                    xe = (XmlElement)groupbox2_xl[0];
                    this.oPage.getUrlTextBox().Text = xe.GetAttribute("value");
                    xe = (XmlElement)groupbox2_xl[1];

                    if (xe.GetAttribute("value") == "POST")
                    {
                        this.oPage.getRequestType_cb().SelectedIndex = 1;
                        xe = (XmlElement)groupbox2_xl[2];
                        this.oPage.getRequestCheckBox().Checked = Boolean.Parse(xe.GetAttribute("value"));
                        xe = (XmlElement)groupbox2_xl[3];
                        this.oPage.getResponseCheckBox().Checked = Boolean.Parse(xe.GetAttribute("value"));
                        xe = (XmlElement)groupbox2_xl[4];
                        this.oPage.add_requestbody_tb(xe.GetAttribute("value"));
                    }
                    else
                    {
                        this.oPage.getRequestType_cb().SelectedIndex = 0;
                        this.oPage.remove_requestbody_tb();
                        xe = (XmlElement)groupbox2_xl[2];
                        this.oPage.getRequestCheckBox().Checked = Boolean.Parse(xe.GetAttribute("value"));
                        xe = (XmlElement)groupbox2_xl[3];
                        this.oPage.getResponseCheckBox().Checked = Boolean.Parse(xe.GetAttribute("value"));
                    }
                }
                else if (xe.GetAttribute("id").ToString() == "json_groupbox")
                {
                    XmlNode json_groupbox_xn = xe.ChildNodes[0];
                    xe = (XmlElement)json_groupbox_xn;
                    this.oPage.getPreviewTextbox().Text = xe.GetAttribute("value");
                }
                else if (xe.GetAttribute("id").ToString() == "3")
                {
                    XmlNodeList groupbox3_xn = xn.ChildNodes;

                    this.oPage.getPara_list().Clear();

                    for (int i = 0; i < groupbox3_xn.Count; i++)
                    {
                        XmlElement paraElement = (XmlElement)groupbox3_xn[i];
                        XmlNodeList paraXml = paraElement.ChildNodes;
                        paraName_tb = new TextBox();
                        paraValue_tb = new TextBox();
                        paraType_tb = new ComboBox();
                        paraDataType_cb = new ComboBox();
                        Para para = new Para(paraName_tb, paraValue_tb, paraType_tb, paraDataType_cb);

                        para.getParaNameTextBox().Text = paraXml.Item(0).InnerText;
                        para.getParaValueTextBox().Text = paraXml.Item(1).InnerText;
                        
                        para.getParaDataTypeComboBox().Text = paraXml.Item(3).InnerText;

                        this.oPage.draw_Para_Component(para.getParaNameTextBox(), para.getParaValueTextBox(), para.getParaTypeComboBox(), para.getParaDataTypeComboBox(),  paraXml.Item(2).InnerText, i);
                        Main.f++;
                        this.oPage.getPara_list().Add(para);
                        this.oPage.getPara_list().TrimExcess();
                    }
                }
                else if (xe.GetAttribute("id").ToString() == "sign_groupbox")
                {
                    XmlNode sign_groupbox_xn = xe.ChildNodes[0];
                    xe = (XmlElement)sign_groupbox_xn;
                    this.oPage.getSignValue_text().Text = xe.GetAttribute("value");
                }
                else if (xe.GetAttribute("id").ToString() == "4")
                {
                    XmlNode groupbox4_xn = xe.ChildNodes[0];
                    xe = (XmlElement)groupbox4_xn;
                    this.oPage.getResponse_ta().Text = xe.GetAttribute("value");
                }
            }
        }

        private void createGroupbox1(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox1LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox1LevelElement.SetAttribute("id", "1");
            rootElement.AppendChild(groupbox1LevelElement);

            XmlElement groupbox1_secondLevelElement_chebox2 = myXmlDoc.CreateElement("key_textBox");
            groupbox1_secondLevelElement_chebox2.SetAttribute("value", this.oPage.getKeyText());

            groupbox1LevelElement.AppendChild(groupbox1_secondLevelElement_chebox2);
        }

        private void createGroupbox2(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox2LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox2LevelElement.SetAttribute("id", "2");
            rootElement.AppendChild(groupbox2LevelElement);

            XmlElement groupbox2_secondLevelElement_url_tb = myXmlDoc.CreateElement("url_tb");
            groupbox2_secondLevelElement_url_tb.SetAttribute("value", this.oPage.getUrlTextBoxValue());
            XmlElement groupbox2_secondLevelElement_requestType_cb = myXmlDoc.CreateElement("requestType_cb");
            groupbox2_secondLevelElement_requestType_cb.SetAttribute("value", this.oPage.getRequestType_cb().Text);
            XmlElement groupbox2_secondLevelElement_requestSwitch_cb = myXmlDoc.CreateElement("requestSwitch_cb");
            groupbox2_secondLevelElement_requestSwitch_cb.SetAttribute("value", this.oPage.getRequestCheckBox().Checked.ToString());
            XmlElement groupbox2_secondLevelElement_resposneSwitch_cb = myXmlDoc.CreateElement("resposneSwitch_cb");
            groupbox2_secondLevelElement_resposneSwitch_cb.SetAttribute("value", this.oPage.getResponseCheckBox().Checked.ToString());

            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_url_tb);
            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_requestType_cb);
            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_requestSwitch_cb);
            groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_resposneSwitch_cb);

            if (oPage.getRequestType_cb().Text == "POST")
            {
                XmlElement groupbox2_secondLevelElement_requestbody_tb = myXmlDoc.CreateElement("requestbody_tb");
                groupbox2_secondLevelElement_requestbody_tb.SetAttribute("value", this.oPage.getRequestbody_tb().Text);
                groupbox2LevelElement.AppendChild(groupbox2_secondLevelElement_requestbody_tb);
            }
        }

        private void createJson_groupbox(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement json_groupboxLevelElement = myXmlDoc.CreateElement("json_groupbox");
            json_groupboxLevelElement.SetAttribute("id", "json_groupbox");
            rootElement.AppendChild(json_groupboxLevelElement);

            XmlElement json_groupbox_secondLevelElement_preview_response = myXmlDoc.CreateElement("preview_response");
            json_groupbox_secondLevelElement_preview_response.SetAttribute("value", this.oPage.getPreviewTextbox().Text);

            json_groupboxLevelElement.AppendChild(json_groupbox_secondLevelElement_preview_response);
        }

        private void createSign_groupbox(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement sign_groupboxLevelElement = myXmlDoc.CreateElement("groupbox");
            sign_groupboxLevelElement.SetAttribute("id", "sign_groupbox");
            rootElement.AppendChild(sign_groupboxLevelElement);

            XmlElement sign_groupbox_secondLevelElement_signValue_text = myXmlDoc.CreateElement("signValue_text");
            sign_groupbox_secondLevelElement_signValue_text.SetAttribute("value", this.oPage.getSignValue_text().Text);

            sign_groupboxLevelElement.AppendChild(sign_groupbox_secondLevelElement_signValue_text);
        }

        private void createGroupbox3(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox3LevelElement = myXmlDoc.CreateElement("groupbox");
            groupbox3LevelElement.SetAttribute("id", "3");
            rootElement.AppendChild(groupbox3LevelElement);

            foreach(Para para in this.oPage.getPara_list())
            {
                XmlElement groupbox3_secondLevelElement_para = myXmlDoc.CreateElement("Para");

                XmlElement groupbox3_thirdLevelElement_paraName = myXmlDoc.CreateElement("paraName");
                groupbox3_thirdLevelElement_paraName.InnerText = para.getParaName();
                XmlElement groupbox3_thirdLevelElement_paraValue = myXmlDoc.CreateElement("paraValue");
                groupbox3_thirdLevelElement_paraValue.InnerText = para.getParaValue();
                XmlElement groupbox3_thirdLevelElement_paraType = myXmlDoc.CreateElement("paraType");
                groupbox3_thirdLevelElement_paraType.InnerText = para.getParaType();
                XmlElement groupbox3_thirdLevelElement_paraDataType = myXmlDoc.CreateElement("paraDataType");
                groupbox3_thirdLevelElement_paraDataType.InnerText = para.getParaDataTypeComboBox().Text;

                groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraName);
                groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraValue);
                groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraType);
                groupbox3_secondLevelElement_para.AppendChild(groupbox3_thirdLevelElement_paraDataType);


                groupbox3LevelElement.AppendChild(groupbox3_secondLevelElement_para);
            }
        }

        private void createGroupbox4(XmlDocument myXmlDoc, XmlElement rootElement)
        {
            XmlElement groupbox4LevelElement = this.myXmlDoc.CreateElement("groupBox");
            groupbox4LevelElement.SetAttribute("id", "4");
            rootElement.AppendChild(groupbox4LevelElement);

            XmlElement groupBox4_secondLevelElement_response_ta = myXmlDoc.CreateElement("response_ta");
            groupBox4_secondLevelElement_response_ta.SetAttribute("value", this.oPage.getResponse_ta().Text);

            groupbox4LevelElement.AppendChild(groupBox4_secondLevelElement_response_ta);

            myXmlDoc.Save(myXMLFilePath);
        }
    }
}
