import { GoogleLogin } from '@react-oauth/google';
import {useNavigate} from "react-router-dom";
import axios from 'axios';
import { toast  ,ToastContainer} from "react-toastify";
import { tokenType } from '../Interfaces/RidesType';
const GoogleLoginForm = () => {
    const navigate = useNavigate();
    const addUser =async ()=>{
      try {
        const token = localStorage.getItem('jwt-token');
        const  headers = {
          "Content-type": "application/json; charset=UTF-8",
          "Authorization": 'Bearer ' + localStorage.getItem('jwt-token')
        }
        const res = await axios.post(`https://localhost:7256/google/login`, {
         token : token
        } ,{ headers :  headers});
        localStorage.setItem("jwt-token" , res.data);
        res && navigate("/bookorofferride");
        toast.success("Signup successfull");
      } catch (err) {
        toast.error("Email Already registered");
      }
    }
    return (
        <GoogleLogin
            onSuccess={credentialResponse => {
              if(credentialResponse.credential!=null){
                console.log(credentialResponse);
                localStorage.setItem("jwt-token",(credentialResponse.credential));
                addUser();
              }
            }}
            onError={() => {
              console.log('Login Failed');
            }}
          />
    )
}

export default GoogleLoginForm;