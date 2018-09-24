# Chương 1: Tổng quan

* Mục đích:
	* Tạo ra ứng dụng cho các bạn học sinh, sinh viên có nhu cầu tra từ tiếng anh chính xác và tốt hơn.

* Các chức năng chính:
    * Tra các từ tiếng Anh
	* Phát âm các từ đã tra
	* Mỗi từ đều có ví dụ cách sử dụng của từ đó bằng một câu tiếng anh mẫu
	* Có thể click vào một từ trong phần định nghĩa để dẫn đến định nghĩa của từ đó
	* Cập nhật các từ tiếng anh mới
	* Sửa hoặc bổ sung các từ đã có trong từ điển
	* Autocomplete phần đuôi khi gõ phần đầu của từ cần tra
	* Khi tra một từ, gợi ý các từ loại khác liên quan (động từ, danh từ, tính từ...)
	* Khi nhập một từ tiếng anh không có trong từ điển, gợi ý các từ gần giống với từ đã tra
	* Sau mỗi một khoảng thời gian, pop up ở taskbar một từ trong danh sách để học t.a
	* Các từ đã tra được lưu vào một tab lịch sử

## Các bước thiết kế chương trình từ điển

* Database:
    * Tạo Database:
        * Tổ chức dữ liệu theo SQLite. Lí do nhóm chọn SQLite là:
        * Việc tổ chức dữ liệu cho từ điển khá quan trọng, và hơn hết là làm sao tổ chức thuận lợi cho quá trình tìm kiếm. Do đó nhóm thực hiện đã chọn cách tổ chức theo SQLite trên nền Json
        * Cấu trúc SQLite : được tổ chức với cặp (WordID,Definition)
            * WorID: Là từ mà người dùng muốn tra
            * Definition: là một mảng(1 file Json) chứa giá trị tương ứng với mỗi thành phần đó bao gồm: ID, Similar, Pronunciation, references, definitions, extra_example, idioms, other_results(Phân tích).
            * Cách tạo file Json: chúng em dùng python script scrap dữ liệu trên trang oxford
                * def save(word, path):
                    """ write data to path in json format with filename is word id """
                    if word is not None:
                        filename = word['id']
                        cache_path = os.path.join(path, filename + '.json')
                        touch(cache_path)


                        with open(cache_path, 'w') as file:
                            json.dump(word, file, separators=(',', ':')) # minify
            * Dữ liệu sẽ được sắp xếp như thế này:
                        (HÌNH ẢNH)

        * Tối ưu hoá database:
            * Khoang vùng, thu nhỏ phạm vi tìm kiếm
	        * Hàm x (chức năng....)
	        * Ví dụ : khi người dùng gõ từ hello vào ô tìm kiếm, hàm x sẽ cắt lấy kí tự đầu tiên là h và trả về các từ có kí tự đầu tiên là h.
	        * Nếu người dùng gõ từ không có nghĩa thì richtextbox sẽ hiển thị những từ gần giống như vậy.
            * ....
* Kiến trúc chương trình:
    * Mô hình hoạt động chính của chương trình
    * Mô tả và giải thích xử lý các lớp, đối tượng phục vụ cho hoạt động của chương trình:
        * Nạp dữ liệu vào form (Giải thích)
		* Tìm kiếm dữ liệu Giải thích)
			* Search Từ.
            * Khi chức năng tra từ được sử dụng: các sự kiện Click, Enter, doublie click vào          TopIndesListView,
		    * Lưu lại lịch sử các từ đã tìm kiếm ( Giải thích)
		    * Audio(Giải thích)
		    * Hình ảnh minh hoạ cho từ (Giải thích)
* Các thiết kế giao diện màn hình:
    * Giao diện chính ( hình ảnh + cách làm)
	* Giao diện Search từ (hình ảnh + cách làm)
* Cài đặt và thử nghiệm:
    * Cài đặt trên windows 10 với ngôn ngữ cài đặt là Microsoft Visual C# setup project 2017 (tạo     file setup).
	* Kết quả:( sau khi hoàn thành )

### Kết luận và hướng phát triển:

* Những khó khăn, thuận lợi khi thực hiện chương trình
* Ưu điểm:
* Nhược điểm:
* Hướng phát triển trong tương lai:
* Phân công thực hiện:
* Tổng kết:
* Tài liệu tham khảo: