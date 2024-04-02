'use client'

// src/button.tsx
import * as React from "react";
import {
  TouchableOpacity,
  StyleSheet,
  Text
} from "react-native";
function Button({ text, onClick }) {
  return /* @__PURE__ */ React.createElement(TouchableOpacity, { style: styles.button, onPress: onClick }, /* @__PURE__ */ React.createElement(Text, { style: styles.text }, text));
}
var styles = StyleSheet.create({
  button: {
    maxWidth: 200,
    textAlign: "center",
    borderRadius: 10,
    paddingTop: 14,
    paddingBottom: 14,
    paddingLeft: 30,
    paddingRight: 30,
    fontSize: 15,
    backgroundColor: "#2f80ed"
  },
  text: {
    color: "white"
  }
});
export {
  Button
};
