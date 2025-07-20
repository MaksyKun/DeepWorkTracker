# Deep Work Tracker
The Deep Work Tracker is a tool inspired by Cal Newports passionate book [Deep Work - Rules for Focused Success in a Distracted World](https://www.thalia.de/shop/home/artikeldetails/A1037756009) providing insides about the fading skill of going deep to produce at high quality and value. In his quoted words:

```
`Deep Work` is the ability to focus without distraction on a cognitively demanding task. [...] Deep work will make you
better at what you do, let you achieve more in less time and provide the sense of true fulfillment that comes from the
mastery of a skill. In short, deep work is like a superpower in our increasingly competitive economy.
```

This application aims to allow for precise tracking of deep working sessions, as well as evaluating those. Furthermore, It's secondary goal is to minimize the possibility of distraction through shallow work like mail checking, meetings etc. by simply restricting your local access on those resources while the device is enabled for deep work.

## Mathematical Explanation of tracking deep work in the approach
Cal Newport claims that the formula of `High-Quality Work Produced = (Time Spent) * (Intensity of Focus)` is valid for determining how much value the results of a deep work session might return. While I aggree with that formula by now, I was still asking myself how to exactly determine those variables to track my quality.
Therefore, the following extension of mine exists to define them.


In order to make useful metrics I had to define some raw metrics to track each session which lead to:
- `Date`: Simply the date when a deep work session was practiced
- `Start`: The start time when you begin your deep work session
- `End`: The end time when you stop your deep work session
- `Context Switches`: The amount of distractions or 'shallow work' that discrupted you during your session
- `Tasks`: The amount of finished (inside the same context) tasks inside you session
- `Output`: The pragmatic value you procuded in form of `Codelines`, `Words` or `Executions`.
  - Executions are kind of generic for now and open for further suggestions to part other types of work out.
- `Focus Score`: Your personal rating from 0-10 on how productive your session was. It can be entered after the session is end. This score has no use yet but might be potential for comparance between subjective and objective `High-Quality Work Produced`.

Now lets come to some formulas:
- `Deep Work Hours` = `End - Start` (Calculated by end - start time of your session). That value is equivalent to `Time Spent`, yet I just named it `Deep Work Hours` to use them from metrics outside of `High-Quality Work Produced`.
- `Intensity of Focus` = `(Tasks * Output ) / ( Context Switches + 1 )`
- Now that the variables are set, we can calculate the `High-Quality Work Produced (HQWP)`. However, that metric leads to an absolute result thats hardly possible to compare to each other, so I aggreed on the following:
  -  Each `10 units` of a `HQWP` result in `1%`, meaning `1000 units = 100%`
  
This way, everything is set up to calculate your deep work sessions into metrics like in the following example:
<img width="913" height="170" alt="image" src="https://github.com/user-attachments/assets/48fba9f5-1462-4df9-bd89-4cf213d479b6" />

In this case, the initial session achieved only a quality of 59,38%, while the second one went even worst with only 39,65%. The last one (they are all fictional) however achieved a rate of 113,75%, having a massive improvement towards the others. As you can see, the `Context Switches` are becoming a harsch penalty factor for the quality of your produced work so it becomes natural to prevent those as much as possible.


### Limitations of the current approach
While the `Output` of `Codelines` and `Words` works perfectly out, lower values like from `Executions` (lets say around 60 units) lead to a big negative effect on the quality because the whole formula is meant to work with greater output units for greater results. This is one problem that in fact should be considered and fixed in future.



## Current Roadmap

### Closer Tasks
- Create a proper frontend to view deep work sessions and allow for several metrics.
- Make a tracking button to let the device 'go deep' as well by counting the time from start to end.
- While practicing deep work, allow to enter your self-reflected `Context Switches`-

### Further Tasks
- Provide support to track event-based usage of distractable products, aiming to do so for Office365 on Outlook, Teams for now
- Auto count `Context Switches`, when the device user has responded to anything on those applications

