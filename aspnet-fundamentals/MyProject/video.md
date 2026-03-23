# 📺 Course Companion: ASP.NET Core MVC (Bhrugen Patel)

This guide follows the steps of the **DotNetMastery (Bhrugen Patel)** YouTube tutorial. Use this to track your progress and understand the "CLI version" of the Visual Studio steps shown in the video.

---

## ✅ Phase 1: Database Setup (Completed)
*You have already finished these steps in your project.*

1.  **Create the Model:** Create a `Category.cs` class in the `Models` folder.
2.  **Setup DbContext:** Create `ApplicationDbContext.cs` to act as the bridge to the database.
3.  **Connection String:** Add the SQL Server connection string in `appsettings.json`.
4.  **Register Service:** Add `builder.Services.AddDbContext<...>` in `Program.cs` (Dependency Injection).
5.  **Push to DB:** Run migrations to create the actual table in SQL Server.
    - `dotnet ef migrations add InitialCreate`
    - `dotnet ef database update`

---

## 🛠️ Phase 2: Displaying Data (The "Read" in CRUD)
*Next steps in the video: Showing the Category list on a web page.*

### Task 2.1: Create the Category Controller
In MVC, the Controller handles the logic. It talks to the Database and tells the View what to show.
- **Action:** Create `Controllers/CategoryController.cs`.
- **Key Logic:** Inject the `ApplicationDbContext` via the constructor and use `_db.Categories.ToList()` to get data.

### Task 2.2: Create the Index View
- **Action:** Create `Views/Category/Index.cshtml`.
- **Key Logic:** Use a `@model IEnumerable<Category>` at the top to tell the page it will receive a list of categories.
- **HTML:** Create a table and use a `@foreach` loop to display each category.

---

## 📝 Phase 3: Creating Data (The "Create" in CRUD)
*Allowing users to add new categories from the browser.*

### Task 3.1: Create Action (GET)
- **Goal:** Show the empty form to the user.
- **Action:** Add `public IActionResult Create()` to the `CategoryController`.

### Task 3.2: Create View
- **Goal:** The HTML form.
- **Action:** Create `Views/Category/Create.cshtml`.
- **Tip:** Use Tag Helpers like `asp-for="Name"` and `asp-for="DisplayOrder"`.

### Task 3.3: Create Action (POST)
- **Goal:** Receive the form data and save it to SQL.
- **Action:** Add `[HttpPost]` version of the `Create` method.
- **Logic:** `_db.Categories.Add(obj); _db.SaveChanges();`.

---

## 🛡️ Phase 4: Validation & UI Polish
*Making sure the data is correct and the app looks professional.*

### Task 4.1: Server-Side Validation
- **Action:** In the POST action, use `if (ModelState.IsValid)` to check if the user filled the form correctly based on the `[Required]` attributes in your Model.

### Task 4.2: Client-Side Validation
- **Action:** Add the `_ValidationScriptsPartial` to your Create view so errors show up instantly without reloading the page.

### Task 4.3: Styling with Bootswatch
- **Action:** Go to [Bootswatch.com](https://bootswatch.com/), download a theme (like "Cyborg" or "Lux"), and replace the `wwwroot/css/bootstrap.min.css` file.

---

## 🔄 Phase 5: Edit & Delete
*Updating and removing records.*

### Task 5.1: Edit (Update)
- Similar to Create, but you must pass the **ID** to find the specific category: `_db.Categories.Find(id);`.

### Task 5.2: Delete
- Use `_db.Categories.Remove(obj);` followed by `_db.SaveChanges();`.

---

## 🚀 Pro Tips for this Course

1.  **CLI vs. GUI:** When Bhrugen clicks "Add -> Controller", you manually create the `.cs` file. It's better for your learning!
2.  **Dependency Injection:** Notice how we don't say `new ApplicationDbContext()`. We ask .NET to provide it for us in the constructor. This is a core professional concept.
3.  **Namespaces:** Always ensure your `using` statements at the top match your folder structure (e.g., `using MyProject.Models;`).
4.  **GitHub:** Commit your changes after every "Phase" so you can go back if you make a mistake.

---
*Follow these steps as you watch the video. If you get stuck on a specific task, ask me!* 💻
