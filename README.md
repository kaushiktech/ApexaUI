So I just finished assigned work. I've included basic CRUD operations of Getting a list of advisors, getting a specific advisor, deleting, creating and updating.
I heard you guys work with Ember.js and I kind of tried to make an UI based on ember based on a quick crash course on ember that I youtubed over the weekend.


So basically, some of the features that I included are:-
1) Added JWT based token auth allowing an admin user to login, and this admin would have rights to create/update/delete advisor records. This admin is seeded into the database, using  a password set up
   in config/env variable/azure key.
3) Anyone could list or view the certain advisor.
4) The admin user would be seeded and have a password based on setting APEXA_ADMIN_PASSWORD, i've tied it with config, but on production scenario ideally we would set up as env variable or azure key.
   So setting setx APEXA_ADMIN_PASSWORD "Password here"
5) Also because there is JWT auth, we also need to add a setx APEXA_JWT_KEY "Auth Key", a key to make that auth possible.
6) I found learning Ember a bit of learning curve as there were a few guides unlike react based on youtube, is a bit quirky for me with me working on mvc.net most of my life, but it took some time for me to get through.
7) One thing that stumped me for a while was why indexing constraints were not working, then it hit me after a while, plus confirming it from other sources, and me well I had never worked with in memory database before
   indexes the standard way does not work with it, so had to reorganize the business logic to do an explicity pull and check.
8) Because of well how i find ember works which is new, i had to use some non standard ways of parsing the response error json to show in the form. I couldnt find a proper source for form validation on ember side.

Dev Environment only swagger

![image](https://github.com/user-attachments/assets/4c039c4b-42ff-4dd1-b599-dec46c4115df)

Also basic UI, to create/update/list etc.


![image](https://github.com/user-attachments/assets/e15228c6-3267-4759-bf33-49d375d62e53)

Let me know what you think and we can go over the code sections.


