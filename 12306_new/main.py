# -*- coding: utf-8 -*-
'''
Created on 2013��12��8��

@author: 1987wing
'''
from handleImage import HandleImage
from kyfw import Kyfw
from threadpool import ThreadPool
import threading, sys

fileHandle = open('station.txt')
station = fileHandle.read()
fileHandle.close()

def getStationCode(city):
    city_code = station.split(city + '|')[1].split('|')[0]
    return city_code

from_city = '上海'
to_city = '北京'
    
from_station_telecode = getStationCode(from_city)
to_station_telecode = getStationCode(to_city)

kyfw = Kyfw('zhang1987wing', 'woai12306', from_station_telecode, to_station_telecode, '2014-03-12', 'G104', 'ze_num')
kyfw.setIdentity('胡潇楠', '150104198907060619', 'ze_num', '1')# yz_num：硬座，yw_num：硬卧，ze_num：二等座, rw_num: 软卧
#kyfw.setIdentity('廖臻', '360502198811281319', 'ze_num', '3') #student = 3, adult = 1
#kyfw.setIdentity('余萍', '362226198506300620', 'yw_num', '1')
#kyfw.setIdentity('周火金', '362226196310240620', 'yw_num', '1')
#kyfw.setIdentity('张超', '612321198210242113', 'ze_num', '1')


handleImage1 = HandleImage()
handleImage1.handle_remote_img(kyfw.getLoginCaptcha())  
randCode = raw_input()
login_success = kyfw.loginAysnSuggest(randCode)
while login_success:    
    handleImage1.handle_remote_img(kyfw.getLoginCaptcha())  
    randCode = raw_input()
    login_success = kyfw.loginAysnSuggest(randCode)
kyfw.login()

def reConfirm_step_1():
    #leftTicket_success = kyfw.queryLeftTicket('0X00')
    leftTicket_success = kyfw.queryLeftTicket()
    #kyfw.queryLeftTicketForBeijing()
    while leftTicket_success:
        #leftTicket_success = kyfw.queryLeftTicket('0X00')
        leftTicket_success = kyfw.queryLeftTicket()
    kyfw.submitOrderRequest()
    kyfw.initDC()
    #sys.exit()
    handleImage1.handle_remote_img(kyfw.getPassengerCaptcha())
    randCode = raw_input()
    checkOrderInfo_success = kyfw.checkOrderInfo(randCode)
    while checkOrderInfo_success:
        handleImage1.handle_remote_img(kyfw.getPassengerCaptcha())
        randCode = raw_input()
        checkOrderInfo_success = kyfw.checkOrderInfo(randCode)
    #kyfw.getQueueCount('0X00')#学生票：0X00
    kyfw.getQueueCount()
    #kyfw.confirmSingleForQueue(randCode, '0X00')#学生票：0X00
    kyfw.confirmSingleForQueue(randCode)

def reConfirm_step_2():
    result = kyfw.student_confirmSingleForQueue(randCode)
    if result != "True":
        reConfirm_step_1()

if __name__ == '__main__':
    reConfirm_step_1()
    #reConfirm_step_2()
    P = []
    for i in range(10):
        new_thread = threading.Thread(target = reConfirm_step_1())
        new_thread.setDaemon(True)
        new_thread.start()
        P.append(new_thread)
    for i in range(10):
        P.get(i).join()
    #print time.ctime() 
    #print time.strftime("%a %b %d %Y", time.localtime())