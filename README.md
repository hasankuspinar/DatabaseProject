# Customer and Support Management System

## Overview

This project is a combined system for managing customer shipments and providing real-time support chat functionality. It is built using **ASP.NET Core MVC** with **Firebase Firestore** as the backend for the support chat.

## Features

### Customer and Shipment Management:
- Create and view customers and their associated companies.
- Manage shipments and delete them using stored procedures.

### Support Chat:
- Users can send messages with a subject and view admin replies.
- Admins can view all user messages and respond in real-time.

## Technologies Used
- **Frontend**: ASP.NET Core MVC
- **Database**: MySQL (for customer and shipment management) and Firebase Firestore (for support chat).
- **Real-Time Communication**: Firebase Firestore listeners.
- **Scripting**: JavaScript (Firebase SDK).

## Usage

### Customer and Shipment Management:
1. Use the **Customer** and **Shipment** pages to manage customers and shipments.
2. Create customers with associated companies and view their shipments.
3. Delete shipments via the Delete Shipment page.

### Support Chat:
1. Navigate to the **User Chat** page to send messages to the admin.
2. Admins can respond to messages on the **Admin Chat** page, with replies visible to users in real-time.

## Future Enhancements
1. Add authentication for better security in both modules.
2. Combine customer and support features into a unified dashboard.
3. Improve the UI for both the admin and user chat interfaces.
