# MemoryRefresher
Code for the game Automnal on itch.io that tracks and saves player actions using a notebook interface
Firstly there are two main parts of the Memory Refresher code that I worked on: The notebook, and the time passing code. The notebook code involves the files NotebookData.json, notebook.cs, and SaveData.cs.

NotebookData.json saves notes that the user writes when using the notebook.
When the user writes text in the notebook and closes it, SaveData.cs saves the text into NotebookData.json.
When reopening the game/notebook UI, notebook.cs reads NotebookData.json and formates it correctly, inserting each piece of text into the notebook UI in the right slot.
This was fairly simple to implement as I had experience using JSON files to save and insert data into Unity.

Then the time passing code tracks how long it has been since the user last saved and prints out a message in a text box remarking on how long it has been since the user has last played, with added fluff text if it has been a particularly long or short time. This was somewhat harder to implement as I had to do research on how to track time specifically. Luckily, when researching I found there was a DateTime class built into C#: https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=net-6.0. DateTime objects can track months, days, hours, minutes, and even other variables related to time which I ended up not using such as milliseconds.

One issue I had was that I intended for there to be a message in the case of the player's first time playing that said "You have never played the game before" but wasn't able to get this to work. It was very difficult to test as well since I essentially tried to check if the PlayerSave json existed or not (and if not, create it and print that message) but I would have had to delete the json file in my local data each time. Because of difficulty testing that out, we didn't catch that it didn't work until very late into production and ended up leaving it as it is-there is no problem creating new jsons and saving data, but the message doesn't show up and there is just a blank text box.

This is handled mostly in CheckTime.cs but also involves the files PlayerSave.json and SaveData.json.
In SaveData.cs, the date and time are saved into PlayerSave.json when the player presses save in-game. PlayerSave.json has the variables year, month, day, and minute. It is important to note that the time is not saved automatically and will not be recorded if the player quits the game without saving.
CheckTime.cs is called upon re-opening the game. In CheckTime.cs, the information from PlayerSave.json is reformatted into a DateTime object. Next, another DateTime object is created with the information of the current time(of the computer system the game is being played on, so if your device is in a specific time zone or set to be ahead, it will take that into account) and the first DateTime object is subtracted from the second later DateTime. Luckily, subtraction is a built in DateTime function, or I would have had to manually subtract months and days from each other, which gets very tricky when dealing with long periods of time since not every month or even year is the same length. Another issue I had trouble figuring out was that when subtracting two DateTime objects, rather than returning another DateTime object, it returns a TimeSpan object: https://docs.microsoft.com/en-us/dotnet/api/system.timespan?view=net-6.0 . Because I assumed the subtraction would result in a DateTime object, I started trying to call DateTime functions on the result only to get errors. Through extensive debugging and print statements, I did manage to find out that it was actually a TimeSpan and properly use the object.
Then, based on how long it has been, the game prints a message.

-Default message - “It’s been x days and y hours since you last saved the game”

-Over one year - “Wow! Over a year since you last played!”

-Over one month - “It’s been over a month since you last played. That’s a long time!”

-Less than one day - “It’s been x hours and x minutes since you last saved the game.”

-Less than one hour - “That was quick! It’s been x minutes since you last saved.”
