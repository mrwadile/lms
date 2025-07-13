# Library Management Portal (LMS Lite)

## 🚀 Quick Setup

1. **Clone & Database Setup**
   ```bash
   git clone https://github.com/mrwadile/lms
   cd lms
   ```
   - Create database in SQL Server
   - Run `library_schema.sql` script
   - All Sql query create table query,seed data and store procedure available in file
   - Just need to execute in sql server

2. **Configure the Connection String** in the Web.config file located in the Library.Web folder/project.
   ```xml
   <connectionStrings>
     <add name="LMSLiteDb" connectionString="Server=localserver;Database=LMSLiteDb;Trusted_Connection=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

3. **Build & Run**
   - Open solution in Visual Studio 2019/2022
   - Build solution (Ctrl+Shift+B)
   - Run project (F5)

## 🛠 Tech Stack

- **Backend**: ASP.NET MVC 5 (.NET Framework 4.8)
- **Database**: ADO.NET with SQL Server
- **Frontend**: Bootstrap 5, jQuery, Chart.js
- **Architecture**: 2-project structure
  - Library.Web - MVC project
  - Library.Core - Class library project

### Key Test Cases
- **Book Management**: Add/edit with AJAX validation
- **Issue/Return**: Complete transaction cycle
- **Dashboard**: Chart data accuracy
- **Reports**: Overdue calculations

## 🎯 Technical Decisions

- **ADO.NET over EF**: Better performance and learning experience
- **AJAX-first**: No page reloads, modern UX
- **2-project split**: Separation of concerns
- **Bootstrap + jQuery**: Rapid development with wide support

## 🐛 Troubleshooting

- **DB Connection**: Check SQL Server running, verify connection string
- **AJAX Errors**: Check browser console, verify [HttpPost] attributes
- **Charts Not Loading**: Ensure Chart.js referenced, check console errors
- **Validation Issues**: Verify jQuery validation scripts included
---

**Ready to use! Check logs in ~/logs/errors.txt for any issues.**
