# IPT 7.1 -- Naval superiority calculator

_**PROJECT STATUS: SECOND YEAR PROJECT**_

## The IPT Modules

Students of the IMS (Informatikmittelschule Luzern), eng. (College for Computer Science Lucerne) are different to conventional apprentices by lacking practical and empirical experience, which is highly valued in the IT sphere. The administration sought to solve the evident lack of practice by implementing special subjects under the abbreviation IPT (Integrierter Praxis Teil), eng. (Integrated Practical Part). The students will have their theoretical knowledge (Modules) balanced with empirical and practical knowledge and experience (IPT-Modules) by solving exercises, or, which is commonly the case, by programming their own projects within the scope of the module, both training their ability to lead a  project, while reinforcing their knowledge within them.

### IPT 7.1
**IPT 7.1** is partially paired with the module **M165**, which focuses on **basic work with NoSQL databases**  using **MongoDB**. The Project was developed during the **second semester of the second year**.

## Project description

This project, being based upon a game, seeks to simulate a naval battle, while also calculating the "naval supremacy" of one's fleet compared to the navy of the opposing force. The application features a simple login system, where one may log in and create accounts. In the main UI, the navies of both sides may be inputed. A simple saving and loading system is also featured for the application, to save the configuration of the navies.

## Notable remarks

### Setting up the application
In order to run the application, a **MongoDB database is needed**. Withing the **DBController** class, one may insert their own **MongoDB connection string**. 

The database is required to have the contents of the **Data.json** file within it, saved under the collection name **Ships**.

The **following collections** are also needed: **SaveToShip, SaveSlots, Logins**

### Credit towards group members.

**The classes within "DamageAlgorithmProto"** have **not** been made by me. It has been made by my group member.

The **UI** was made in collaboration with my group member. Thus, it is a **shared** effort.

## Dependencies

A proper MongoDB database with the aforementioned collections and data.
