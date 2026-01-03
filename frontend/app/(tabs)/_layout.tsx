import { View, Text } from 'react-native'
import React from 'react'
import { Tabs } from 'expo-router'

const TabIcon = ({ focused, icon, title }: any) => {
    if (focused){
        return (
            <View className="flex flex-row flex-1 min-w-[112px] min-h-16 mt-4 items-center justify-center rounded-full overflow-hidden bg-accent">
              <Text className="text-white size-5">{icon}</Text>
              <Text className={`text-secondary ml-2 font-semibold ${focused ? "text-accent" : "text-gray-500"}`}>
                {title}
              </Text>
            </View>
        )
    }
    return (
        <View className="size-full justify-center items-center mt-4 rounded-full">
            <Text className="text-white size-5">{icon}</Text>
        </View>
    )
  
}
const _Layout = () => {
  return (
    <Tabs
        screenOptions={{
            tabBarShowLabel: false,
            tabBarItemStyle: {
                width: '100%',
                height: '100%',
                justifyContent: 'center',
                alignItems: 'center',
            },
            tabBarStyle: {
                position: 'absolute',
                height: 52,
                backgroundColor: '#0f0D23',
                borderRadius: 50,
                marginHorizontal: 20,
                marginBottom: 36,
                overflow: 'hidden',
                borderWidth: 1,
                borderColor: '#0f0D23',
            },
        }
            }>
        <Tabs.Screen name="index" options={{ 
            title: 'Home',
            headerShown: false,
            tabBarIcon: ({ focused }) => <TabIcon icon="🏠" title="Home" focused={focused} />,
             }} />
        <Tabs.Screen name="search" options={{ 
            title: 'Search',
            headerShown: false,
            tabBarIcon: ({ focused }) => <TabIcon icon="🔍" title="Search" focused={focused} />,
             }} />
        <Tabs.Screen name="saved" options={{ 
            title: 'Saved',
            headerShown: false,
            tabBarIcon: ({ focused }) => <TabIcon icon="💾" title="Saved" focused={focused} />,
             }} />
        <Tabs.Screen name="profile" options={{ 
            title: 'Profile',
            headerShown: false,
            tabBarIcon: ({ focused }) => <TabIcon icon="👤" title="Profile" focused={focused} />,
             }} />
        <Tabs.Screen name="activeOrders" options={{ 
            title: 'Active Orders',
            headerShown: false,
            tabBarIcon: ({ focused }) => <TabIcon icon="🚚" title="Orders" focused={focused} />,
             }} />
    </Tabs>
  )
}

export default _Layout