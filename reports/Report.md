# CHƯƠNG 1. HIỆN TRẠNG VÀ YÊU CẦU TỪ THỰC TẾ 

## Hiện trạng vấn đề 

### Vấn đề 
* Ngày nay, cùng với sự phát triển của đất nước, các ngành khoa học, ngành công nghệ thông tin,…. Đều sử dụng rất nhiều tài liệu tiếng anh. Trong hệ thống giáo dục của Việt Nam hầu hết các trường từ tiểu học, trung học, phổ thông đến các trường cao đẳng-đại học đều đào tạo bộ môn: Tiếng Anh.
* Do vậy, với mục tiêu nhằm hỗ trợ việc học cho học sinh, sinh viên, việc giảng dạy cho giáo viên ở các trường, hỗ trợ việc biên dịch tài liệu cho các ngành khoa học, công nghệ thông tin, các cơ quan chức năng…. Để đỡ tốn thời gian và công sức ngồi tra từng từ trong một cuốn từ điển dày mấy trăm trang, với sự phát triển không ngừng của ngành công nghệ thông tin các phần mềm từ điển Anh –Anh tiện dụng, hiệu quả cao ra đời ngày càng nhiều. 
* Ngôn ngữ lập trình là một phần không thể thiếu trong việc xây dựng nên một thế giới công nghệ linh hoạt và mạnh mẽ. Không gian làm việc Microsoft .Net tổng hợp bởi bốn bộ ngôn ngữ lập trình: C#, VB.NET, Managed C++, and J# .NET. ở đó có sự chồng gối lên nhau của các ngôn ngữ, và được định nghĩa trong FCL (framework class library). Hỗ trợ cho lập trình viên phát triển các ứng dụng mạng với kích thước nhẹ và mạnh mẽ trong xử lý.
* Dựa trên kiến thức lập trình mạng với C#, sự đa dạng của các dịch vụ mạng với nhiều tính năng và đòi hỏi ngày càng cao. Từ ý tưởng áp dụng công nghệ thông tin vào học tập, giúp cho việc học tập tiếng anh trở nên dễ dàng hơn, đồ án này hướng đến xây dựng một ứng dụng từ điển, áp dụng cho các nhu cầu học tập tiếng anh.
### Phương hướng giải quyết 
## Hiện trạng cơ sở vật chất và con người 
* Tin học 
* Con người         
## Yêu cầu sơ bộ phần mềm 
* Tin học 
* Con người 
# Chương 2. Phân tích yêu cầu phần mềm và mô hình hoá    
## Yêu cầu phần mềm 
### Yêu cầu chức năng
* Bảng tổng hợp và định danh các yêu cầu:   

| Định danh  |               Hàm                  | Độ ưu tiên  | Mô tả yêu cầu                                                          |
|:----------:|------------------------------------|:-----------:|------------------------------------------------------------------------|  
| Yêu cầu 1  | SelectDefinitionFrom(string wordID)|    5        |Tìm kiếm từ vựng trong database của phần mềm                            |   
| Yêu cầu 2  | SearchFromInput()                  |    5        |Hiển thị định nghĩa của từ khi người dùng tra từ                        |   
| Yêu cầu 3  | PlayAudio()                        |    4        |Phát âm từ vựng đã tra theo Anh–Anh và Anh-Mỹ                           |   
| Yêu cầu 4  | UpdateOtherResultList()            |    4        |Khi tra một từ, gợi ý các từ loại khác liên quan (động từ, danh từ, tính từ...)|
| Yêu cầu 5  | UpdateWordlistTopIndex()           |    3        |Autocomplete phần đuôi khi gõ phần đầu của từ cần tra                   |   
| Yêu cầu 6  | CorrectWord()                      |    3        |Khi nhập một từ tiếng anh không có trong từ điển, gợi ý các từ gần giống với từ đã tra|
| Yêu cầu 7  | UpdateHistory()                    |    3        |Các từ đã tra được lưu vào một tab lịch sử                              |   
| Yêu cầu 8  | SearchFromHighlight()              |    3        |Chọn từ trong danh sách từ hiển thị ngay định nghĩa của từ              |
| Yêu cầu 9  | CanSearchFromSelection()	          |    3        |Click vào một từ trong phần định nghĩa để dẫn đến định nghĩa của từ đó  |
### Yêu cầu phi chức năng 

|   Định danh	|   Độ ưu tiên	|   Mô tả yêu cầu	|
|---------------|---	        |---	            |
|   	        |   	        |   	            |
|   	        |   	        |   	            |
|   	        |   	        |   	            |

### Bảng FURPS

| Tiêu chí chất lượng  |  Mô tả                                                                                     |
|----------------------|--------------------------------------------------------------------------------------------|
| Functionality        | •	Chương trình hướng tới phục vụ người dùng đơn lẻ và hoàn toàn miễn phí                  |
| Usability            | •	Giao diện được thiết kể phẳng, hiện đại, đơn giản và dễ sử dụng, Chức năng tra cứu từ điển thông minh, tiện dụng, Cung cấp kèm theo tài liệu hướng dẫn và chức năng của chương trình|
| Reliability          | •	Chương trình lấy dữ liệu từ nguồn tin cậy (Oxford Learners Dictionaries) và được cập nhật liên tục|
| Performance          | •	Việc tra cứu từ nhanh và chính xác, cách sắp xếp định nghĩa  dễ hiểu và khoa học        |
| Supportability       | •	Database được tổ chức ở dạng chuẩn, sắp xếp khoa học                                    |

## Mô Hình hoá 
### Các trường hợp sử dụng thông thường 

|  Use Case | Tên         | Mô tả                           | Yêu cầu liên quan         |
|---        |---          |---                              |---                        |
| UC 1      | Tra cứu từ  | Tra cứu định nghĩa từ, phát âm  | Search, SpellCheck, Audio |
|           |             |                                 |                           |
|           |             |                                 |                           |

#### Use case 1 (Tra cứu từ) 
![alt text](https://scontent.fsgn5-4.fna.fbcdn.net/v/t1.15752-9/32550241_656380638037814_973409897611788288_n.png?_nc_cat=0&_nc_eui2=AeEnVgEJX5A1aPRIa7Dk4ZQ7Ckxw8EgF9CSdSKyKqfDaFpyVkq6cwX0YR6fd_q-tt3sdfBzp5gSIpmuXFxT8yUm4jg8qU1wIbgyfP6g6l8iymQ&oh=6409cc9389ba2fffc4702d90e68c4192&oe=5B780998)
### Mô hình thực thể - mối quan hệ 

# Chương 3. Thiết kế 
## Thiết kế kiến trúc phần mềm 
Chương trình sẽ được thiết kế theo mô hình kiến trúc MVVM:
* View: Là phần giao diện của ứng dụng để hiển thị dữ liệu và nhận tương tác của người dùng. Một điểm khác biệt so với các ứng dụng truyền thống là View trong mô hình này tích cực hơn. Nó có khả năng thực hiện các hành vi và phản hồi lại người dùng thông qua tính năng binding, command.
* View Model: Lớp trung gian giữa View và Model. ViewModel có thể được xem là thành phần thay thế cho Controller trong mô hình MVC. Nó chứa các mã lệnh cần thiết để thực hiện data binding, command. 
* Model: Là các đối tượng giúp truy xuất và thao tác trên dữ liệu thực sự.

![alt text](https://scontent.fsgn5-4.fna.fbcdn.net/v/t1.15752-9/32762652_656808434661701_4704287531963777024_n.png?_nc_cat=0&_nc_eui2=AeFeiPapcFCPWJ4SxSr68lj-kmHz00e13wH9N38rIzSYjo7V9M5Kf4VXIMHmlyZ5b4-GAZ4d2FTHxxF9h9k6MrGy3pPqnSgL3Wku_4BAzdifOA&oh=ea4be55b99e4cff5116794033767a41a&oe=5B9048EB)
## Thiết kế dữ liệu 
### Tổng quan 
![alt text](https://scontent.fsgn2-3.fna.fbcdn.net/v/t1.15752-9/32901750_656814607994417_3674429974397321216_n.png?_nc_cat=0&oh=780c5ed9f1d125aec7ea6178f38462f0&oe=5B91EA96)

* ID là phần định danh giữa các từ với nhau. Các từ trùng tên nhưng có nhiều wordform (như từ "work" vừa là động từ vừa danh từ) thì phần name vẫn là chữ work, còn bên ID sẽ chèn thêm số để phân biệt (vd động từ là work_1 còn danh từ là work_2)

![alt text](https://raw.githubusercontent.com/ThanhLoc47/imageproject/master/work_1.PNG)
![alt text](https://raw.githubusercontent.com/ThanhLoc47/imageproject/master/work_2.PNG)

* Name là cột tên của từ mà người dùng sẽ nhìn thấy trong cột autocomplete của từ điển.
* Definition là tất cả các định nghĩa, idiom, vd và các reference liên quan về từ có ID đó
### Cách tạo database
* Tổ chức dữ liệu theo SQLite. Lí do nhóm chọn SQLite là: …..
    * Việc tổ chức dữ liệu cho từ điển khá quan trọng, và hơn hết là làm sao tổ chức thuận lợi cho    quá trình tìm kiếm. Do đó nhóm thực hiện đã chọn cách tổ chức theo SQLite trên nền Json
    * Cấu trúc SQLite : được tổ chức với cặp (WordID,Definition)
        * WorID: Là từ mà người dùng muốn tra
        * Definition: là một mảng(1 file Json) chứa giá trị tương ứng với mỗi thành phần đó bao gồm: ID, Similar, Pronunciation, references,      definitions, extra_example, idioms,other_results(Phân tích).
    * Cách tạo file Json: Dùng python script scrap dữ liệu trên trang oxford và chuyển dữ liệu vào file Json
    ```python
    def save(word, path):
	""" write data to path in json format with filename is word id """
	if word is not None:
		filename = word['id']
		cache_path = os.path.join(path, filename + '.json')
		touch(cache_path)

		with open(cache_path, 'w') as file:
			json.dump(word, file, separators=(',', ':')) # minify
    ```
    * Giải thích chi tiết cách lấy dữ liệu:
    * Dữ liệu sẽ được sắp xếp như thế này: 

    ![alt text][logo]

    [logo]: https://raw.githubusercontent.com/ThanhLoc47/imageproject/master/work_2.PNG
    
    * Giải thích (lấy ví dụ là từ letter):
        * id: từ vựng (ví dụ: letter_1 (noun))
        * similar: các loại từ khác liên quan (động từ, danh từ, tính từ) (ví dụ: letter_2 (verb))
        * pronunciatons: phát âm theo Anh-Anh và Anh-Mỹ, đường dẫn file audio
        * references: gồm các từ liên quan và các thành ngữ liên quan gắn với từ đó 
        * definitions: bao gồm tất cả các định nghĩa của 1 từ và ví dụ của từng định nghĩa đó 
            * Ví dụ: 
                * Định nghĩa: a message that is written down or printed on paper and usually put in an envelope and sent to somebody
                * Ví dụ của định nghĩa: a business/thank-you, etc. letter
        * extra_examples: Là phần ví dụ bổ sung cho từ
            * Ví dụ:
                * A letter headed ‘Advertising Mania’ appeared in the paper.
                * Apart from the occasional letter, they had not been in touch for years.
                * Fill in the form in block letters.
        * idioms: bao gồm các thành ngữ và định nghĩa, ví dụ cho mỗi thành ngữ đó
            * Ví dụ:
                * Thành ngữ: the letter of the law
                * Định nghĩa cho thành ngữ:  the exact words of a law or rule rather than its general meaning
                * Ví dụ cho thành ngữ: They insist on sticking to the letter of the law
        * other_results: bao gồm các từ liên quan đi kèm với từ    
## Thiết kế giao diện và thành phần xử lí của giao diện
* Giao diện chương trình 

![](https://raw.githubusercontent.com/ThanhLoc47/imageproject/master/giaodien.PNG)

* Mô tả thành phần giao diện: 
* Giao diện search chương trình

![](https://raw.githubusercontent.com/ThanhLoc47/imageproject/master/giaodienkhisearch.PNG)