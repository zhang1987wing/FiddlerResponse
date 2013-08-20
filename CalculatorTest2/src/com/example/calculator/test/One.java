package com.example.calculator.test;

import android.test.ActivityInstrumentationTestCase2;

import com.jayway.android.robotium.solo.Solo;

public class One {
	public void test(Solo solo, ActivityInstrumentationTestCase2 a)
	{
		solo.clickOnButton("登录");
		a.assertTrue(solo.searchText("密码找回"));
		
		solo.enterText(0, "zhang1987wing");
		solo.enterText(1, "1987wingyi");
		
		solo.clickOnButton("登录");
		
		a.assertTrue(solo.searchText("用户信息"));
		
		solo.clickOnButton("用户信息");
		a.assertTrue(solo.searchText("zhang1987wing"));
		solo.clickOnButton("注销");
		a.assertTrue(solo.searchText("切换帐号"));
		solo.clickOnButton("确定");
		try
		{
			a.assertTrue(solo.searchText("用1户信息"));
		}
		catch (Exception e)
		{
			System.out.println(e.toString());
		}
	}
}
