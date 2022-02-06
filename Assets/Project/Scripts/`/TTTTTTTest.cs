using UnityEngine;

public class TTTTTTTest : MonoBehaviour
{
	private void OnEnable()
	{
		//Print1();
		//Print2();
		//Print3();
		//Print4();
		//Print5();
		Print6();
		Print7();
		Print8();
		Print9();
	}

	void Print1()
	{
		print("#1");

		int a = 100;
		int b = 9;
		int c = 27;

		double d = (double)a / (double)b;
		int e = (int)(c * d);

		print(d);
		print(e);
	}

	void Print2()
	{
		print("#2");

		int a = 100;
		int b = 9;
		int c = 27;

		double d = a / (double)b;
		int e = (int)(c * d);

		print(d);
		print(e);
	}

	void Print3()
	{
		print("#3");

		int a = 100;
		int b = 9;
		int c = 27;

		double d = (double)a / b;
		int e = (int)(c * d);

		print(d);
		print(e);
	}

	void Print4()
	{
		print("#4");

		int a = 100;
		int b = 9;
		int c = 27;

		double d = a / b;
		int e = (int)(c * d);

		print(d);
		print(e);
	}

	void Print5()
	{
		print("#5");

		int a = 100;
		int b = 9;
		int c = 27;

		double d = (double)(a / b);
		int e = (int)(c * d);

		print(d);
		print(e);
	}

	void Print6()
	{
		print("#6");

		int a = 107;
		int b = 13;
		int c = 24;

		double d = (double)a / (double)b * (double)c;

		print(d);
	}

	void Print7()
	{
		print("#7");

		int a = 107;
		int b = 13;
		int c = 24;

		double d = (double)a / b * c;

		print(d);
	}

	void Print8()
	{
		print("#8");

		int a = 107;
		int b = 13;
		int c = 24;

		double d = a / (double)b * c;

		print(d);
	}

	void Print9()
	{
		print("#9");

		int a = 107;
		int b = 13;
		int c = 24;

		double d = a / b * (double)c;

		print(d);
	}
}