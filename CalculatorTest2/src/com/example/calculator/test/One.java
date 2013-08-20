package com.example.calculator.test;

import android.test.ActivityInstrumentationTestCase2;

import com.jayway.android.robotium.solo.Solo;

public class One {
	public void test(Solo solo, ActivityInstrumentationTestCase2 a)
	{
		solo.clickOnButton("��¼");
		a.assertTrue(solo.searchText("�����һ�"));
		
		solo.enterText(0, "zhang1987wing");
		solo.enterText(1, "1987wingyi");
		
		solo.clickOnButton("��¼");
		
		a.assertTrue(solo.searchText("�û���Ϣ"));
		
		solo.clickOnButton("�û���Ϣ");
		a.assertTrue(solo.searchText("zhang1987wing"));
		solo.clickOnButton("ע��");
		a.assertTrue(solo.searchText("�л��ʺ�"));
		solo.clickOnButton("ȷ��");
		try
		{
			a.assertTrue(solo.searchText("��1����Ϣ"));
		}
		catch (Exception e)
		{
			System.out.println(e.toString());
		}
	}
}
