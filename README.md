# Bookstore Web Application  

This project was developed as part of the course **"Web Programming and Web Design"**.  
The task was to create a fully functional web application using **ASP.NET Core** with the **MVC pattern**, including both client-side and server-side components, on a specific topic. The chosen topic was **"Bookstore"**.  

## Project Features  

The application was required to include a **database** with tables that establish various types of relationships between them.  

Additionally, user roles were implemented to ensure different access levels and permissions within the system. **Authentication and authorization** were also incorporated, utilizing the **ASP.NET Identity** infrastructure to meet these requirements.  

To fully implement the bookstore functionality, a **Stripe payment system** was integrated to handle transactions.  

## Main Features  

- **User account system**:  
  - The administrator can create users with different roles.  
  - The administrator can block access for specific users if necessary.  

- **Management of categories, books, and companies**:  
  - Adding, editing, and deleting entities.  

- **Shopping cart and payment system**:  
  - Users can add books to their cart and complete purchases using Stripe.  

- **Order management system**:  
  - Updating order details, changing order statuses, or canceling orders.  
  - If an order is canceled, the payment is automatically refunded to the buyer.  

## Automated Testing  

To ensure the stability and reliability of the application, **automated tests** were written using **NUnit** and **Selenium WebDriver**.  

---

This application provides a comprehensive bookstore system with secure authentication, role-based access control, and an integrated payment gateway, ensuring a seamless user experience.  
