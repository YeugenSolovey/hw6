using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Program
{
    public class PublisherHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public PublisherHouse(int id, string name, string adress)
        {
            Id = id;
            Name = name;
            Adress = adress;
        }
    }

    public class Book
    {
        public int PublishingHouseId { get; set; }
        public string Title { get; set; }
        public PublisherHouse PublishingHouse { get; set; }

        public Book(int publishingHouseId, string title, PublisherHouse publishingHouse)
        {
            PublishingHouseId = publishingHouseId;
            Title = title;
            PublishingHouse = publishingHouse;
        }
    }

    class Program
    {
        public static async Task Main(string[] args)
        {
            List<Book> books = new List<Book>();

            List<Book> bos = new List<Book>();
            string path_red = @"C:\hw6\hw6_json.json";
            string path_clone = @"C:\hw6\h6_json_clone.json";
            string path_write = @"C:\hw6\hw6_json_write.json";
            //read our json file
            using (FileStream fs = new FileStream(path_red, FileMode.Open, FileAccess.Read))
            {
                books = await JsonSerializer.DeserializeAsync<List<Book>>(fs);
                foreach (Book book in books)
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.Out.WriteLine("Title: " + book.Title + "\nId: " + book.PublishingHouseId + "\nPublishingHouse( " + "\n    Adress: " + book.PublishingHouse.Adress + "\n    Name: " + book.PublishingHouse.Name + "\n    Id: " + book.PublishingHouse.Id + "\n)\n");
                    bos.Add(new Book(book.PublishingHouseId, book.Title, new PublisherHouse(book.PublishingHouse.Id, book.PublishingHouse.Name, book.PublishingHouse.Adress)));
                }
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            };
            //clone our json 
            using (FileStream fstream = new FileStream(path_clone, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, bos, options);
                Console.WriteLine("books was clone");
            }
            books.Clear();
            books.Add(new Book(1, "k", new PublisherHouse(1, "ol", "kyiv")));
            books.Add(new Book(2, "a", new PublisherHouse(1, "as", "kyiv")));
            books.Add(new Book(3, "b", new PublisherHouse(1, "er", "kyiv")));

            //add json element to new file
            using (FileStream fstream = new FileStream(path_write, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, books, options);
                Console.WriteLine("books was writed");
            }
        }
    }
}