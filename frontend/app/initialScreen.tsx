import { View, Text, TouchableOpacity } from 'react-native'
import React from 'react'

interface InitialScreenProps {
    onClose: () => void;
}
const InitialScreen: React.FC<InitialScreenProps> = ({onClose}) => {
  return (
    <View className="flex-1 justify-center items-center bg-primary px-5">
        <Text className="text-white text-lg text-center mb-5">
          Project "Programowanie Aplikacji Mobilnych" - 2025 - 
          Eryk Olsza, 14331
        </Text>
        <Text className="text-white text-sm text-center mb-10">
            This project is a part of the course "Programowanie Aplikacji Mobilnych" at WSEI. 
            It is not intended for commercial use and is solely for educational purposes.
        </Text>
        <Text className="text-white text-sm text-center mb-10">
            The app is desined as a browser for interesting articles (called posts in the app) with minimal design.
        </Text>
        <TouchableOpacity
          onPress={onClose}
          className="bg-accent px-5 py-3 rounded-full"
        >
          <Text className="text-white font-bold">I Understand</Text>
        </TouchableOpacity>
      </View>
  )
}

export default InitialScreen