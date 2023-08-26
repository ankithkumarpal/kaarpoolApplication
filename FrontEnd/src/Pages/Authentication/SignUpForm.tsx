import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { Link , useNavigate} from "react-router-dom";
import axios from "axios";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import "./signupform.scss";
import { signUpFormType } from "../../Interfaces/AuthenticationType";
import { signupUrl } from "../../HttpServices/HttpUrls";
import { toast  ,ToastContainer} from "react-toastify";
import SpinnerIcon from '@rsuite/icons/legacy/Spinner';
import { GoogleOAuthProvider } from '@react-oauth/google';
import GoogleLoginForm from "../../Components/GoogleLoginForm";


export const SignUpForm = () => {

  let [email, setEmail] = useState<string>("");
  let [password, setPassword] = useState<string>("");
  let [conformPassword, setConformPassword] = useState<string>("");
  let [buttonClicked , setButtionClicked] = useState<boolean>(false);
 
  const navigate = useNavigate();
  const {
    register,
    formState: { errors },
    handleSubmit,
    watch,
  } = useForm<signUpFormType>({ mode: "onTouched" });
  const onSubmit: SubmitHandler<signUpFormType> = async (data) => {
    setButtionClicked(!buttonClicked);
    try {
      const res = await axios.post(`${signupUrl}`, {
        name: "ankith",
        phoneNo: "9738529",
        email: email,
        password: password,
      });
      res && navigate("/login");
      toast.success("Signup successfull");
    } catch (err) {
      toast.error("Email Already registered");
      setButtionClicked(false);
    }
  };

  return (
    <div
      className={`signupform d-flex align-items-center justify-content-center w-100 orange-shade-bg`}
    >
    <ToastContainer/>
      <div className="inner-signupform  h-100 mt-5 d-flex flex-column align-items-center justify-content-center">
        <div className="signup-header d-flex flex-column align-items-center justify-content-center">
          <h1>Sign Up</h1>
          <p></p> 
        </div>
        <div className="mt-3 signup-form">
          <Form onSubmit={handleSubmit(onSubmit)}>
            <Form.Group
              className="mb-3 input-feild form-floating"
              controlId="formBasicEmail"
            >
              <Form.Control
                type="email"
                id="email"
                placeholder="Enter Email Id"
                className="pt-3 pb-3 form-input"
                size="lg"
                {...register("email", {
                  required: true,
                  pattern: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                })}
                onChange={(e) => setEmail(e.target.value)}
              />
              {errors.email?.type == "required" && <p>"Email is required"</p>}
              {errors.email?.type == "pattern" && <p>"invalid email format"</p>}
              <label htmlFor="email" className="text-muted">
                Enter the Email Id
              </label>
            </Form.Group>
            <Form.Group
              className="mb-3 input-feild form-floating"
              controlId="formBasicPassword"
            >
              <Form.Control
                type="password"
                id="password"
                placeholder="Password"
                className="pt-3 pb-3"
                {...register("password", {
                  required: true,
                  minLength: 3,
                  maxLength: 10,
                })}
                onChange={(e) => setPassword(e.target.value)}
              />
              {errors.password?.type == "required" && (
                <p>Password is required</p>
              )}
              {errors.password?.type == "minLength" && (
                <p>password min size should be 3</p>
              )}
              {errors.password?.type == "maxLength" && (
                <p>Password max size should be 10</p>
              )}
              <label htmlFor="password" className="text-muted">
                Password
              </label>
            </Form.Group>
            <Form.Group
              className="mb-3 input-feild form-floating"
              controlId="formBasicPassword"
            >
              <Form.Control
                type="password"
                id="conformpassword"
                placeholder="Conform Password"
                className="pt-3 pb-3"
                {...register("confirmPassword", {
                  required: true,
                  validate: (value: string) => watch("password") == value,
                })}
                onChange={(e) => setConformPassword(e.target.value)}
              />
              {errors.confirmPassword?.type == "validate" && (
                <p>password is doesn't match</p>
              )}
              <label htmlFor="confrompassword" className="text-muted">
                Conform Password
              </label>
            </Form.Group>
            <div className="signup-button d-flex align-items-center justify-content-center w-100">
              <Button
                variant="primary"
                type="submit"
                className={`violet-shade-bg`}
                disabled = {buttonClicked ? true : false}
              >
                {
                  buttonClicked ?
                  <>
                  Submit
                  <SpinnerIcon pulse style={{ fontSize: '1em' , marginLeft: "10px" }} />
                </> : <> Submit</>
                }
              </Button>
            </div>
          </Form>
        </div>
        <div className="signup-text d-flex align-items-center justify-content-center mt-5">
          <p>Already a member?</p>
          <p className="login-text">
            <Link to="/login"> Log In</Link>
          </p>
        </div>
       <GoogleOAuthProvider clientId="996243699729-71lo3s5d0g0s02cffun2gsrf1jrlu3fe.apps.googleusercontent.com">
          <GoogleLoginForm/>
        </GoogleOAuthProvider>
      </div>
    </div>
  );
};
