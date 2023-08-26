import './timestamp.scss';
import { useEffect, useState } from 'react';
import { timeStampType } from '../Interfaces/RidesType';
export const TimeStamp = ({setTime , date} : timeStampType) => {

  let [selectedTime , setSelectedTime] = useState<string>('');
  const handleTimeStampClick = (slot : string , time : string)=>{
     if(date == ""){
      alert("select date");
      return ;
     }
     setTime(time);
     setSelectedTime(slot);
  }
  let currentTime = new Date().getHours();
  let selectedDate = (date.split("-")[1]);
   const validate = ()=>{
    console.log("faslflafjlk")
       let dateArr = date.split("-");
       let currDate = new Date();
       console.log(parseInt(dateArr[1]))
       console.log(currDate.getMonth())
       if(parseInt(dateArr[2]) == currDate.getDate()
        &&  parseInt(dateArr[1]) == currDate.getMonth() + 1
        && parseInt(dateArr[0]) == currDate.getFullYear()) return true;
        console.log("reached")
        return false;
   }
  return (
    <>
     <div className='timestamp w-100 h-100'>
        <p className='text-secondary mt-1'>Time</p>
        <div className="time-container d-flex flex-wrap  align-items-start">
        <div className={`time-div d-flex align-items-center justify-content-center mb-1 me-1 
            ${selectedTime == '1' ? 'selected-time':''} ${date != "" && validate() == true &&   currentTime >= 9 ? 'blur-feild' : ''}`}
            onClick ={()=>handleTimeStampClick("1" , "'5am - 9am")}>5am - 9am</div>
            <div className={`time-div d-flex align-items-center justify-content-center mb-1 me-1
            ${selectedTime == '2' ? 'selected-time':''} ${date != "" && validate() == true &&  currentTime  >= 12 ?'blur-feild' :''}`}  onClick ={()=>handleTimeStampClick("2" , "'9am - 12am")}>9am - 12pm</div>
             <div className={`time-div d-flex align-items-center justify-content-center mb-1 me-1
            ${selectedTime == '3' ? 'selected-time':''} ${date != ""&&  validate() == true &&   currentTime >= 15 ? 'blur-feild' : ''}`}onClick ={()=>handleTimeStampClick("3" , "'12pm - 3pm")}>12pm - 3pm</div>
             <div className={`time-div d-flex align-items-center justify-content-center mb-1  me-1
             ${selectedTime == '4' ? 'selected-time':''} ${date != "" &&   validate() == true &&  currentTime >= 18  ? 'blur-feild' : ''}`} onClick ={()=>handleTimeStampClick("4" , "'3pm - 6pm")}>3pm - 6pm</div>
             <div className={`time-div d-flex align-items-center justify-content-center mb-1 me-1
            ${selectedTime == '5' ? 'selected-time':''} ${date != "" &&  validate() == true &&   currentTime >= 21  ? 'blur-feild' :''}`} onClick ={()=>handleTimeStampClick("5" , "'6pm - 9pm")}>6pm - 9pm</div>
             <div className={`time-div d-flex align-items-center justify-content-center mb-1 me-1
            ${selectedTime == '6' ? 'selected-time':''} ${date != "" &&  validate() == true &&  currentTime > 23 ? 'blur-feild' : ''}`} onClick ={()=>handleTimeStampClick("6" , "'9pm - 12am")}>9pm - 12am</div>
        </div>
     </div>
    </>
  )
}