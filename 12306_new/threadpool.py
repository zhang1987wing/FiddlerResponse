# -*- coding: utf-8 -*-
'''
Created on 2014��1��1��

@author: 1987wing
'''
import threading, Queue, time, sys
from kyfw import Kyfw

class ThreadPool:
    def __init__(self, kyfw):
        self.Qin = Queue.Queue()
        self.Qout = Queue.Queue()
        self.Qerr = Queue.Queue() 
        self.kyfw = kyfw
        self.pool = []
    
    def report_error(self):#将错误信息放入Qerr来报告错误
        self.Qerr.put(sys.exc_info()[:2])
    
    def get_all_from_queue(self, Q):#获取队列Q中所有项，无须等待
        try:
            while True:
                yield Q.get_nowait()
        except Queue.Empty:
            raise StopIteration
    
    def do_work_from_queue(self):
        while True:
            command, randCode = self.Qin.get()
            print '2'
            if command == 'stop':
                break
            try:
                if command == 'process':
                    result = self.kyfw.confirmSingleForQueue(randCode)
                    print '1'
                else:
                    raise ValueError, 'Unknow command %r' % command
            except:
                self.report_error()
            else:
                self.Qout.put(result)
    
    def make_and_start_thread_pool(self, number_of_threads_in_pool = 5, daemons = True):
        #创建一个n线程的池子，是所有线程为守护线程，启动所有线程
        for i in range(number_of_threads_in_pool):
            new_thread = threading.Thread(target = self.do_work_from_queue)
            new_thread.setDaemon(daemons)
            self.pool.append(new_thread)
            new_thread.start()
    
    def request_work(self, data, command = 'process'):
        self.Qin.put((command, data))
    
    def get_result(self):
        return self.Qout.get()
    
    def show_all_results(self):
        for result in self.get_all_from_queue(self.Qout):
            print 'Result:', result
    
    def show_all_errors(self):
        for etyp, err in self.get_all_from_queue(self.Qerr):
            print 'Error:', etyp, err
    
    def stop_and_free_thread_pool(self):
        for i in range(len(self.pool)):
            self.request_work(None, 'stop')
        for existing_thread in self.pool:
            existing_thread.join()
        del self.pool[:]