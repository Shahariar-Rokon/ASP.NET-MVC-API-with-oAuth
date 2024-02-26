# ASP.NET MVC API with Master Detail - Hotel Service Reservation API


## Description

This is an ASP.NET MVC api project with oAuth authentication. oAuth is a token based authentication security system. 

In this project, I've created a hypothetical scenario about a software where you can:

- Using oauth token based authentication system based on OWIN
- Save Guest information.
- Add multiple Services that they had used in the hotel.
- Submit data using ASP.NET MVC API.
- Passing Image file/ Picture throught api
- Handle master-details relationships


This project is intended to showcase the features and capabilities of ASP.NET MVC API with oauth token based authentication. Feel free to explore the code and provide any feedback or contributions.

## Installation
To install and run this project, follow these steps:
### Prerequisite
- You need **Postman** or other 3rd party software to test the api.
- You can download post man from https://www.postman.com/downloads/
- You nees Visual Studio (2019 or later).

1. **Clone the repository**: Clone the repository to your local machine using the GitHub CLI command:

   ```shell
   gh repo clone Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation
   
  Alternatively, you can download the ZIP file from the GitHub repository page: https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation.git
  
2. Open the project in Visual Studio 2022: Open Visual Studio 2022 and select “Open a project or solution”. Navigate to the folder where you cloned or downloaded the repository and select the .sln file.
3. Restore the NuGet packages: Right-click on the solution in the Solution Explorer and select “Restore NuGet Packages”. This will install the required dependencies for the project.
4. Update the database connection string: In the appsettings.json file, update the DefaultConnection value with your database connection string. Make sure the database server is running and accessible.
5. Apply the database migrations: In the Package Manager Console, run the following command to apply the code first migrations and create the database schema:
`Update-Database`
6. Run the project: Press F5 or click the “Run” button to launch the project in your browser.
   
## Usage

To use this project, follow these steps:

1. **Navigate to the app**: Open your browser and go to the URL where the app is hosted. For example, `https://localhost:44302/`. You can notice that the browser gives the error code 403.14 forbidden. Because we used oauth token based security.
2. **Interact with the app**:
   - Now while running the app, open the **Postman** and paste the api url for example `https://localhost:44302/`.
   - Now modify the url and write  `https://localhost:44302/token`. 
   - In the **Postman** go to **body** and select **x-wwww-form-urlencoded** as shown in the snippet.
   - ![urlencoded](https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation/assets/116648090/fae8f791-e6ff-423c-bf51-0555051a8c55)
   - Now fill out the form using key-value pairs. The keys should be **grant_type** ,**username** and **password** with corresponding values **password**, **Admin** and **1234**. Note that the username 
     and password are prepopulated in the database. After completing this step, send a **Get** request in postman. This will result in display a valid token as as shown in the screenshot.
     ![login](https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation/assets/116648090/4def0199-0415-42f2-a68d-5875485c8195)
   - Now copy the token and head to the **Authorization** tab and enter authorization type as **Bearer Token** and paste the token in the token box.
     ![token](https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation/assets/116648090/7b772c99-959c-414a-a020-b2983f13ff15)
   - Now do not click the **Send** button yet, just modify the url and write the api/controller name for example `https://localhost:44302/api/Reservation`.
   - Now click the send button. This will authorize you through the oauth to the **GET** and will show some valid data.
     ![data](https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation/assets/116648090/48a1f2a2-4f51-4a66-833a-f8af3edff5d5)
   - Now it's time to post data. Head over to body section and select **form-data** tab. Now fill out the valuse as follows: **Reservation** should be **JSON** object **ImageFIle** which should be file 
     type and finally **ImageFileName** should be a string.
   - For example Reservation should be like this
                 `{
                  "ReservationNo":"A",
                  "ReservationDate":"2024-01-01",
                  "GuestName":"Jhon X",
                  "IsPaid":true,
                  "ReservedItems":[{"ServiceId":1}]
                  }`
   Select an image file from your desktop and give any name for your image file. If all things filled correctly the **Postman** should look like this
   ![post](https://github.com/Shahariar-Rokon/ASP.NET-MVC-API-Hotel-Reservation/assets/116648090/7a3943d2-0d19-4e54-98ee-2d59f623e54a)
 - Next step is to post. Select **POST** in the postman and click send.
 - Now make a **GET** request to test the post.
 - Note that the service is prepopulated and the token has a time limit. If token expires, you need to get another token. 
4. **Explore the code**: You can open the project in Visual Studio 2022 and explore the code. The project consists of **REST** api.

## Contributing

We welcome contributions from anyone who is interested in improving this project. Here are some ways you can contribute:

- Report bugs or suggest features by opening an issue.
- Fix bugs or implement features by submitting a pull request.
- Review pull requests and provide feedback.
- Write or update documentation, tests, or examples.
- Share your experience or feedback with the project.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
