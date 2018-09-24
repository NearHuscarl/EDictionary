Quy ước viết code cho đồ án

* Codebase viết hoàn toàn bằng tiếng anh, Không biết thì dùng từ điển dịch ngược lại
* Tên lớp: PascalCase, là danh từ hoặc cụm danh từ
```c#
// Correct
public class ClientActivity
{
	// code
}
```
* Tên hàm: PascalCase, bắt đầu bằng một động từ miêu tả chức năng của nó
```c#
// Correct
public bool IsAvailable()
{
	// code
}
public float Divide()
{
	// code
}

// Avoid
public bool available()
{
	// code
}
public float division()
{
	// code
}
```
* Tên biến, tham số: camelCase, là danh từ hoặc cụm danh từ
```c#
public int Sum(int operandOne, int operandTwo)
{
	int result = operandOne + operandTwo;
	return result;
}
```
* Hạn chế viết hàm có nhiều hơn 4 tham số (interface lơm thượm)
* Dộ dài hàm: không quá 40 dòng, trường hợp đặt biết thì phải comment tại sao như vậy,
	bắt đầu refactor lại (chia thành từng hàm nhỏ hơn) khi vượt quá 40. Nói chung là không
	bao giờ có 1 hàm mà dài quá chiều cao màn hình laptop
* Tên mảng: thêm s hoặc es vào sau đuôi, là danh từ hoặc cụm danh từ
```c#
int apple;
int[] apples;
```
* Tên hằng: PascalCase, là danh từ hoặc cụm danh từ
```c#
// Correct
public static const string ShippingType = "DropShip";

// Avoid
public static const string SHIPPINGTYPE = "DropShip";
```
* Không sử dụng các từ viết tắt khi đặt tên biến, ngoại trừ các từ đặt biệt thông dụng
```c#
// Correct
UserGroup userGroup;
Assignment employeeAssignment;
StudentId studentId;

// Avoid
UserGroup usrGrp;
Assignment empAssignment;
StudentId studentIdentification;
```
* Tên file: Trùng với tên lớp chính trong file đó
```c#
// Correct
// Filename: Program.cs
class Program
{
	static void Main(string[] args)
	{
		// code
	}
}
```
* Cặp ngoặc nhọn {} phải đặt thẳng hàng với nhau
```c#
// Correct
class Program
{
	static void Main(string[] args)
	{
		// code
	}
}

// Avoid
class Program {
	static void Main(string[] args) {
		// code
	}
}
```
* Sử dụng empty line hợp lý để trình bày code
```c#
// Correct
public float Divide(float a, float b)
{
	float result;

	if (b == 0)
	{
		throw 'Cant divide to 0';
	}
	result = a / b;

	return result;
}

// Avoid
public float Divide(float a, float b)
{


	float result;
	if (b == 0)
	{
		throw 'Cant divide to 0';

	}
	result = a / b;
	return result;
} 
```
* Khai báo tên biến trên cùng của lớp hoặc hàm, biến static đặt trên cùng
```c#
// Correct
public class Account
{
	public static string BankName;
	public static decimal Reserves;

	public string Number {get; set;}
	public DateTime DateOpened {get; set;}
	public DateTime DateClosed {get; set;}
	public decimal Balance {get; set;}

	// Constructor
	public Account()
	{
		// code
	}
}
```
* Sử dụng tab thay vì space
```c#
// Correct
public class ClientActivity
{
	// code
}

// Avoid
public class ClientActivity
{
   // code
}
```
* Các bước viết hàm (tham khảo, không bắt buộc):
	* Thử nghiệm các câu lệnh với nhau
	* Khi nó đã chạy đc hoặc bắt đầu có ý nghĩa, ráp lại thành 1 hàm
	* Khi hàm bắt đầu dài, refactor lại chia thành các hàm con
	* Lặp lại các bước trên
* Hạn chế sử dụng comment: code nên rõ nghĩa ngay từ tên biến, tên hàm và các bước
thực hiện của nó
* Chỉ dùng single line comment, không dùng multi-line comment
```c#
// Use single line
// comment like this

/* Avoid using
  multiline comment  */
```
