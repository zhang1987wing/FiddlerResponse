#coding=utf-8
'''
Created on 2014-1-27

@author: yi.zhang
'''
import base64
import urllib2


f=open(r'remote_save.png','rb')
ls_f=base64.b64encode(f.read())
f.close()

print ls_f

proxyHandler = urllib2.ProxyHandler({'http':'127.0.0.1:9898',
                                     'https':'127.0.0.1:9898',})
opener = urllib2.build_opener(proxyHandler)
urllib2.install_opener(opener)

response = urllib2.urlopen('http://check.huochepiao.360.cn/img_yzm?' + "{%22img_buf%22:%22" + ls_f + "%22, %22check%22:%22394ede2c6516d7ffcf72c5703504012e%22}").read()