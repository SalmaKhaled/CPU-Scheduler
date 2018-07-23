# CPU-Scheduler
A GUI application you give it some processes and burst time and it arrange them according to a scheduling method you choose.

Inputs : Type of scheduler + no of Processes + required information about each process
         according to the scheduler type ( ex : quantum time in round robin).

Output: Time line showing the order and time taken by each process (Gantt Chart) +
        Average waiting time + Turn around time.
        
Types of schedulers supported:
1. FCFS
2. SJF (Preemptive and Non Preemptive)
3. Priority (Preemptive and Non Preemptive) (the smaller the priority number the
   higher the priority)
4. Round Robin  
