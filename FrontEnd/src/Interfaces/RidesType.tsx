export type tokenType ={
    email : string , 
    phoneno : string , 
    id : string 
}

export type matchedRideType = {
  id: Number ,
  sourceName: string ,
  destinationName: string ,
  userId: string ,
  date: string,
  slotTime: string ,
  noOfPassenger: Number 
}

export type stopsType = {
  id : Number , 
  loationId : Number , 
  name : string , 
  occupency : Number
}

export type rideDetailsType = {
  capacity : Number , 
  date : string, 
  destination : string , 
  distance : Number
  fairPerKm  : string  , 
  id : Number , 
  slotTime : string ,
  ownerId : Number , 
  source : string , 
  email : string , 
  stops :  stopsType [], 
  vehicleNumber : Number 
}

export type bookingRequestType = {
  id: Number,
  source: string ,
  destination: string,
  ownerId: Number,
  userId: Number,
  fair: Number,
  noOfPassenger: Number,
}

export type mathchingCardType = {
  rideDetails : rideDetailsType;
  bookedRide : any;
  getAvailableSeats : any;
  getRideFair: any;
}

export type OfferedRideCardType = {
  rideDetails : rideDetailsType;
}

export type BookedRideCardType =  {
  rideDetails : bookRideType;
}

export type timeStampType ={
  date : string 
  setTime : React.Dispatch<React.SetStateAction<string>>
}

export type bookRideType = {
  id : Number,
  email : string , 
  rideId : Number , 
  date : string , 
  slotTime: string , 
  source : string , 
  destination : string , 
  ownerId : Number , 
  userId : Number , 
  fair : Number , 
  noOfSeats : Number , 
  charge : Number 
}

export type bookRideFormType = {
   source : string , 
   destination : string , 
   seats : Number , 
   date : string , 
   time : string 
}