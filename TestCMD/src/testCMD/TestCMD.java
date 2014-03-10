package testCMD;
import java.io.IOException;

public class TestCMD {
	public static void main(String[] args) throws IOException
	{
		Autotest test = new Autotest();
		test.run(args[0]);
		//test.run(args[0]);
		
		//Autotest test = new Autotest();
		//test.run(args[0]);
		//Ç©Ãû
		//jarsigner -verbose -digestalg SHA1 -sigalg MD5withRSA -keystore debug.keystore -storepass android -keypass android demo.apk androiddebugkey
	}
}
