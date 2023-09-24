// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function SetItem(key, value) { localStorage.setItem(key, value);}

export function GetItem(key) { return localStorage.getItem(key); }

export function RemoveItem(key) { localStorage.removeItem(key); }

export function Clear() { localStorage.clear(); }
