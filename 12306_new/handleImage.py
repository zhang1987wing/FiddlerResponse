#coding=utf-8
'''
Created on 2014-1-17

@author: yi.zhang
'''
import Image
import StringIO
import os

class HandleImage:
    def __init__(self):
        self.img_path = ""
    #改变图片大小
    def resize_img(self, img_path):
        try:
            img = Image.open(img_path)
            (width,height) = img.size
            new_width = 200
            new_height = height * new_width / width
            out = img.resize((new_width,new_height),Image.ANTIALIAS)
            ext = os.path.splitext(img_path)[1]
            new_file_name = '%s%s' %('small',ext)
            out.save(new_file_name,quality=95)
        except Exception,e:
            print e
     
    #改变图片类型
    def change_img_type(self, img_path):
        try:
            img = Image.open(img_path)
            img.save('new_type.png')
        except Exception,e:
            print e
     
    #处理远程图片
    def handle_remote_img(self, img_data):
        try:
            img_buffer = StringIO.StringIO(img_data)
            img = Image.open(img_buffer)
            img.save('remote_save.png')
            (width,height) = img.size
            out = img.resize((200,height * 200 / width),Image.ANTIALIAS)
            out.save('remote_small.png')
            #openedImg = Image.open('remote_small.jpg');
            #openedImg.show();
            os.system('start E:\python_script\\12306_new\\remote_small.png')
        except Exception,e:
            print e