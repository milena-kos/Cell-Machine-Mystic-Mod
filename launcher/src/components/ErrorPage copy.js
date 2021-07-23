import { CloseIcon } from "./icons/CloseIcon";
import { AlertIcon } from "./icons/AlertIcon";

export const ErrorPage = ({ errCode }) => {
  <div className="fs-container">
    <CloseIcon
      className="iconBtn top-align"
      onClick={() => window.electron.exit()}
    />
    <AlertIcon />
    <span className="loader-text-err">{errCode}</span>
  </div>;
};
