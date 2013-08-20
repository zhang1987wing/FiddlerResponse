package com.example.calculator.test;
import com.jayway.android.robotium.solo.Solo;
import android.test.ActivityInstrumentationTestCase2;
@SuppressWarnings("unchecked")
public class TestApk extends ActivityInstrumentationTestCase2{
	private static final String LAUNCHER_ACTIVITY_FULL_CLASSNAME="com.tiancity.tsi.activity.MainActivity";
	private static Class launcherActivityClass;
	
	private String test = "√‹¬Î’“ªÿ";
	static{
		try{
			launcherActivityClass = Class.forName(LAUNCHER_ACTIVITY_FULL_CLASSNAME);
		} catch (ClassNotFoundException e){
			throw new RuntimeException(e); } }
	public TestApk()throws ClassNotFoundException{
		super(launcherActivityClass); }
	private Solo solo;
	@Override
	protected void setUp() throws Exception {
		solo = new Solo(getInstrumentation(),getActivity());
	}
	public void testDisplayBlackBox() {
		//Enter any integer/decimal value for first edit-field, we are writing 10
		/*solo.enterText(0, "10");
		//Enter any integer/decimal value for first edit-field, we are writing 20
		solo.enterText(1, "20");
		//Click on Multiply button
		solo.clickOnButton("Multiply");
		//Verify that resultant of 10 x 20
		assertTrue(solo.searchText("200"));*/
		
		/*solo.clickOnButton("µ«¬º");
		assertTrue(solo.searchText(test));
		
		solo.enterText(0, "devtest300");
		solo.enterText(1, "maxmax");
		
		solo.clickOnButton("µ«¬º");
		
		assertTrue(solo.searchText("’À∫≈ªÚ’ﬂ√‹¬Î¥ÌŒÛ"));
		//solo.clickOnbu*/
		
		One one = new One();
		one.test(solo, this);
	}

	public void tearDown() throws Exception {
		solo.finishOpenedActivities();
	}
}