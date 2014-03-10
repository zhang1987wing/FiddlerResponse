# -*- coding: utf-8 -*-
'''
Created on 2013��12��1��

@author: 1987wing
'''
import urllib2, urllib, cookielib
import json, time
from BeautifulSoup import BeautifulSoup as bs4
import socket

class Kyfw:
    def __init__(self, username, password, from_station_telecode, to_station_telecode, date, train, the_seat):
        proxy = urllib2.ProxyHandler({'https':'127.0.0.1:9898'})
        cookiejar = cookielib.CookieJar()
        opener = urllib2.build_opener(proxy, urllib2.HTTPCookieProcessor(cookiejar))
        #opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cookiejar))
        urllib2.install_opener(opener)
        self.username = username
        self.password = password
        self.train = train
        self.from_station_telecode = from_station_telecode
        self.to_station_telecode = to_station_telecode
        self.date = date
        self.the_seat = the_seat
        self.dict = {}
        self.passengerInformation = {}
        self.name = ''
        self.cardno = ''
        socket.setdefaulttimeout(30)
        
    def setIdentity(self, name, cardno, seat_type, passenger_type):
        self.passengerInformation[cardno] = [name, seat_type, passenger_type]
    
    def passengerTicketStr(self):
        passengerTicketStr_string = ''
        for x in xrange(len(self.passengerInformation.keys())):
            if self.passengerInformation[self.passengerInformation.keys()[x]][1] == 'ze_num':
                passengerTicketStr_string += "O,0," + self.passengerInformation[self.passengerInformation.keys()[x]][2] +"," + self.passengerInformation[self.passengerInformation.keys()[x]][0] + ",1," + self.passengerInformation.keys()[x] + ",13524043093,N_"
            elif self.passengerInformation[self.passengerInformation.keys()[x]][1] == 'yw_num':
                passengerTicketStr_string += "3,0," + self.passengerInformation[self.passengerInformation.keys()[x]][2] +"," + self.passengerInformation[self.passengerInformation.keys()[x]][0] + ",1," + self.passengerInformation.keys()[x] + ",13524043093,N_"
            elif self.passengerInformation[self.passengerInformation.keys()[x]][1] == 'rw_num':
                passengerTicketStr_string += "4,0," + self.passengerInformation[self.passengerInformation.keys()[x]][2] +"," + self.passengerInformation[self.passengerInformation.keys()[x]][0] + ",1," + self.passengerInformation.keys()[x] + ",13524043093,N_"
        print self.passengerInformation
        return passengerTicketStr_string
    
    def oldPassengerStr(self):
        oldPassengerStr_string = ''
        for x in xrange(len(self.passengerInformation.keys())):
            oldPassengerStr_string += self.passengerInformation[self.passengerInformation.keys()[x]][0] + ",1," + self.passengerInformation.keys()[x] + "," + self.passengerInformation[self.passengerInformation.keys()[x]][2] + "_"
        print oldPassengerStr_string
        return oldPassengerStr_string
        
    def getLoginCaptcha(self):
        #headers = {"User-Agent":"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.48 Safari/537.36"}
        self.request = urllib2.Request("https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand")
        self.request.add_header('User-Agent', 'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.48 Safari/537.36')
        self.request.add_header('Connection', 'keep-alive')
        try:
            img_data = urllib2.urlopen(self.request).read()
        except Exception:
            return self.getLoginCaptcha() #递归只能使用999次
        return img_data
    
    def login(self, *arg):
        url = 'https://kyfw.12306.cn/otn/index/init'
        self.request = urllib2.Request(url)
        try:
            response = urllib2.urlopen(self.request).read()
        except Exception:
            return self.login()
        soup = bs4(response)
        data = soup.find('a', {'id':'login_user'}).text
        print data
        
    def loginAysnSuggest(self, randCode):
        self.request = urllib2.Request("https://kyfw.12306.cn/otn/login/loginAysnSuggest")
        values = {"loginUserDTO.user_name":self.username,
                  "userDTO.password":self.password,
                  "randCode":randCode}
        try:
            data = urllib2.urlopen(self.request, urllib.urlencode(values)).read()
            data_decode = json.loads(data)
            if str(data_decode["data"]) == "{}":
                print json.dumps(data_decode["data"])
                return True
            else:
                print str(data_decode["data"])
                return False
        except Exception:
            return self.loginAysnSuggest(randCode)
        
      
    def queryLeftTicket(self, purpose_codes = 'ADULT'):
        url = "https://kyfw.12306.cn/otn/leftTicket/query?leftTicketDTO.train_date=" + self.date + "&leftTicketDTO.from_station=" + self.from_station_telecode + "&leftTicketDTO.to_station=" + self.to_station_telecode + "&purpose_codes=" + purpose_codes
        self.request = urllib2.Request(url)
        try:
            data = urllib2.urlopen(url).read()
        except Exception:
            return self.queryLeftTicket()
        data_decode = json.loads(data)
        if str(data_decode["messages"]) == "[]":
            result = data_decode['data']
            print len(result)
            for k in range(0, len(result)):
                if self.train in result[k]['queryLeftNewDTO']['station_train_code']:
                    if result[k]['queryLeftNewDTO'][self.the_seat] == '--' or result[k]['queryLeftNewDTO'][self.the_seat] == '无' or result[k]['queryLeftNewDTO'][self.the_seat] == '*':
                        print '没有你想要的票'
                        return True
                    else:
                        self.dict.setdefault('from_station_name', result[k]['queryLeftNewDTO']['from_station_name'])
                        self.dict.setdefault('to_station_name', result[k]['queryLeftNewDTO']['to_station_name'])
                        self.dict.setdefault('secretStr', result[k]['secretStr'])
                        self.dict.setdefault('yp_info', result[k]['queryLeftNewDTO']['yp_info'])
                        self.dict.setdefault('station_train_code', result[k]['queryLeftNewDTO']['station_train_code'])
                        self.dict.setdefault('from_station_telecode', result[k]['queryLeftNewDTO']['from_station_telecode'])
                        self.dict.setdefault('to_station_telecode', result[k]['queryLeftNewDTO']['to_station_telecode'])
                        self.dict.setdefault('train_no', result[k]['queryLeftNewDTO']['train_no'])
                        return False
        else:
            print json.dumps(data_decode["messages"], ensure_ascii = False)
            return True

    def submitOrderRequest(self, purpose_codes = 'ADULT'):
        url = "https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest"
        try:
            return urllib2.urlopen(url, "secretStr=" + self.dict.get('secretStr') + "&train_date=" + self.date + "&back_train_date=2013-12-09&tour_flag=dc&purpose_codes=" + purpose_codes + "&query_from_station_name=" + self.dict.get('from_station_name') + "&query_to_station_name=" + self.dict.get('to_station_name') + "&undefined").read()
        except Exception, e:
            #return self.submitOrderRequest(purpose_codes)
            print e
    
    def checkUser(self):
        url = "https://kyfw.12306.cn/otn/login/checkUser"
        values = {"_json_att":""}
        data = urllib.urlencode(values)
        try:
            return urllib2.urlopen(url, data).read()
        except Exception:
            return self.checkUser()
    
    def initDC(self):
        url = "https://kyfw.12306.cn/otn/confirmPassenger/initDc"
        values = {"_json_att":""}
        data = urllib.urlencode(values)
        try:
            result = urllib2.urlopen(url, data).read()
            self.token = result.split('globalRepeatSubmitToken = \'')[1].split('\'')[0]
            self.key_check_isChange = result.split('key_check_isChange\':\'')[1].split('\'')[0]
        except Exception, e:
            print e
            return self.initDC()
        print self.key_check_isChange
        print self.token
        
    def getPassengerCaptcha(self):
        self.request = urllib2.Request("https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew.do?module=passenger&rand=randp&0.8210586933419108")
        try:
            img_data = urllib2.urlopen(self.request).read()
        except Exception:
            return self.getPassengerCaptcha()
        return img_data
    
    def queryOrderWaitTime(self):
        self.request = urllib2.Request("https://dynamic.12306.cn/otsweb/order/myOrderAction.do?method=queryOrderWaitTime&tourFlag=dc")
        self.orderId = ""
        try:
            json_text = urllib2.urlopen(self.request).read()
        except Exception:
            return self.queryOrderWaitTime()
        
        if "orderId" in json_text:
            print json_text
            data = json.loads(json_text)
            self.orderId = data['orderId']
        else:
            self.queryOrderWaitTime()
    
    def checkOrderInfo(self, randCode):
        url = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo"
        values = {"REPEAT_SUBMIT_TOKEN":self.token,
                  "cancel_flag":"2",
                  "bed_level_order_num":"000000000000000000000000000000",
                  "passengerTicketStr": self.passengerTicketStr(),
                  "oldPassengerStr": self.oldPassengerStr(),
                  "tour_flag":"dc",
                  "randCode":randCode,
                  "_json_att":""}
        
        data = urllib.urlencode(values)
        try:
            json_data = json.loads(urllib2.urlopen(url, data).read())
            print str(json_data)
            if json_data['data']['submitStatus'] == False:
                print json.dumps(json_data['data']['errMsg'], ensure_ascii = False)
                return True
            else:
                return False
                print json_data
        except Exception:
            return self.checkOrderInfo(randCode)
        
    def getQueueCount(self, purpose_codes = '00'):
        url = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount"
        localtime = time.mktime(time.strptime(self.date + " 00:00:00","%Y-%m-%d %H:%M:%S"))
        values = {"train_date":time.strftime("%a %b %d %Y %H:%M:%S", time.localtime(localtime)) + " GMT+0800 (中国标准时间)",
                  "train_no":self.dict.get('train_no'),
                  "stationTrainCode":self.dict.get('station_train_code'),
                  "seatType":1,
                  "fromStationTelecode":self.dict.get('from_station_telecode'),
                  "toStationTelecode":self.dict.get('to_station_telecode'),
                  "leftTicket":self.dict.get('yp_info'),
                  "_json_att":"",
                  "purpose_codes":purpose_codes,
                  "REPEAT_SUBMIT_TOKEN":""}#self.token}
        
        data = urllib.urlencode(values)
        try:
            print urllib2.urlopen(url, data).read()
        except Exception:
            self.getQueueCount(purpose_codes)
        
        
        
    def confirmSingleForQueue(self, randCode, purpose_codes = '00'):
        url = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue"
        values = {"passengerTicketStr": self.passengerTicketStr(),
                  "oldPassengerStr": self.oldPassengerStr(),
                  "purpose_codes": purpose_codes,
                  "key_check_isChange":self.key_check_isChange,
                  "leftTicketStr":self.dict.get('yp_info'),
                  "train_location":"H3",
                  "randCode":randCode,
                  "_json_att":"",
                  "REPEAT_SUBMIT_TOKEN":""#self.token
        }
        
        data = urllib.urlencode(values)
        try:
            json_data = json.loads(urllib2.urlopen(url, data).read())
        except Exception:
            return self.confirmSingleForQueue(randCode)
        print str(json_data['data']['submitStatus'])
        if json_data['data']['submitStatus'] == False:
            return json.dumps(json_data['data']['errMsg'], ensure_ascii = False)
        else:
            return 'True'