# Neo-Doro

**Neo-Doro** is a modern task management application that reimagines the classic **Pomodoro Technique** with a dynamic and customizable focus system. Designed to enhance productivity, Neo-Doro helps users manage their time effectively by dividing work into structured **projects** and **tasks**. Each task is powered by a **dual-timer system**, optimizing focus periods and strategic rest intervals for long-term productivity without burnout.

## Key Features

### ⚙️ Dual Timer System
Neo-Doro offers two types of timers—**Focus Time** and **Rest Time**—helping users seamlessly switch between work and rest periods to maintain balance.

### ⏲️ Customizable Focus Duration
Users can define their own **focus periods**, such as \(4 \, \text{hours}\), allowing flexibility depending on the nature of their tasks.

### 🛌 Adaptive Rest Periods
Rest periods are calculated based on a **percentage** of the focus period:
\[
\text{Rest Time} = \text{Focus Time} \times \text{Rest Percentage}
\]
For example, if a user works for 4 hours with a rest percentage of 20%, the rest time will be:
\[
\text{Rest Time} = 4 \times 0.2 = 0.8 \, \text{hours} = 48 \, \text{minutes}
\]

### 🔄 Incremental Focus Timer
Unlike traditional countdown timers, Neo-Doro’s **Focus Timer** operates like a stopwatch, **counting upwards** from zero:
\[
t_{\text{focus}} = 0, 1, 2, \ldots, T_{\text{max}}
\]
This reduces stress by focusing on **time invested**, rather than how much time remains.

### ⏳ Countdown Rest Timer
The **Rest Timer** counts **downwards** once a break starts, letting users know exactly when it’s time to resume work:
\[
T_{\text{rest}} \rightarrow 0
\]

### 🚦 Maximum Rest Threshold
Rest periods are capped at **30%** of the total focus time to ensure a balanced workflow:
\[
\text{Max Rest Time} \leq 0.3 \times \text{Focus Time}
\]

---

## How It Works

1. **Create a Project**: Users can create multiple projects, each containing related tasks.
2. **Start a Task**: Set a **focus duration** (e.g., \(4 \, \text{hours}\)) for each task.
3. **Work with Focus**: The **incremental timer** starts counting upwards, motivating users to stay engaged.
4. **Take a Break**: Switch to the **Rest Timer**, which will **count down** based on the focus session.
5. **Maintain Balance**: Ensure the rest duration does not exceed **30%** of the focus period.
