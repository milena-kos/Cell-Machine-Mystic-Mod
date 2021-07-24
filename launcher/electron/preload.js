const { contextBridge, ipcRenderer } = require("electron");
contextBridge.exposeInMainWorld("electron", {
  exit: () => ipcRenderer.send("exit"),

  getData: async () => {
    ipcRenderer.send("resize", { width: 300, height: 300 });
    const repo =
      process.env.CUSTOM_REPO || "Sequitur-Studios/Cell-Machine-Mystic-Mod";
    const launcherData = await get(
      process.env.CUSTOM_LAUNCHER_CONFIG ||
        `https://raw.githubusercontent.com/${repo}/master/launcher_config.json`
    );
    const releaseData = await get(
      `https://api.github.com/repos/${repo}/releases`
    );
    ipcRenderer.send("resize", { width: 600, height: 320 });
    return { releaseData: releaseData, launcherData: launcherData };
  },

  receive: (channel, func) =>
    ipcRenderer.on(channel, (event, ...args) => func(...args)),

  removeListener: (channel, func) =>
    ipcRenderer.removeListener(channel, (event, ...args) => func(...args)),

  launchGame: (data, tag) => ipcRenderer.send("launchGame", { data, tag }),
});

const get = async (u) =>
  await fetch(u).then((r) => {
    if (!r.ok) throw Error("failed to fetch");
    return r.json();
  });
