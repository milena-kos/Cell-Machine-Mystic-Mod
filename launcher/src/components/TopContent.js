import { CloseIcon } from "./icons/CloseIcon";

export const TopContent = () => (
  <div className="top-container">
    <div className="title-container">
      <h1 className="title">Cell Machine</h1>
      <h1 className="secondary-title">Mystic Mod Launcher</h1>
    </div>
    <div className="flex-spacer"></div>
    <div className="btn-container">
      <CloseIcon className="iconBtn" onClick={() => window.electron.exit()} />
      {/* <SettingsIcon className="iconBtn" /> */}
    </div>
  </div>
);
