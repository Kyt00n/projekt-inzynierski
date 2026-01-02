import { View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import { Post } from '@/interfaces/Post'
import { Link } from 'expo-router'

const PostCard = ({id, title, thumbnailUrl }: Post) => {
  return (
    <Link href={`/posts/${id}`} asChild>
        <TouchableOpacity className="w-[30%]">
            <Image source={{ uri: thumbnailUrl }} className="w-full h-16 rounded-lg" resizeMode="cover"/>
            <Text className="text-white text-sm font-semibold mt-2" numberOfLines={1}>{title}</Text>
        </TouchableOpacity>
                
    </Link>


  )
}

export default PostCard