import SearchBar from "@/components/searchBar";
import { ActivityIndicator, FlatList, ScrollView, Text, View } from "react-native";
import { useRouter } from "expo-router";
import useFetch from "@/services/useFetch";
import { getPosts } from "@/services/api";
import PostCard from "@/components/postCard";

export default function Index() {
  const router = useRouter();

  const {
    data: posts,
    loading: postsLoading,
    error: postsError,
  } = useFetch(()=>getPosts({query: ''}))
  return (
    <View className="flex-1 bg-primary">
      <ScrollView className="flex-1 px-5" 
      showsVerticalScrollIndicator={false} 
      contentContainerStyle={{minHeight: "100%", paddingBottom: 10}}>
        <Text className="w-12 h-10 mt-20 mb-5 mx-auto text-center text-4xl">📚</Text>
        <Text className="mx-auto text-center text-4xl text-white font-bold">All Posts</Text>

        {postsLoading ? (
          <ActivityIndicator size="large" color="#0000ff" className="mt-10 self-center" />
        ):
        postsError ? (
          <Text>Error: {postsError?.message}</Text>
        ):
        <View className="flex-1 mt-5">
          <SearchBar
          onPress={() => router.push("/search")}
          placeholder="Search for a post..."
          value=""
          onChangeText={() => {}}
          />

          <>
            <Text className="text-lg text-white font-bold mt-5 mb-3">Latest Posts</Text>

            <FlatList
              data={posts}
              keyExtractor={(item) => item.id.toString()}
              numColumns={3}
              columnWrapperStyle={{ justifyContent: "flex-start", gap:20, paddingRight: 10, marginBottom:10 }}
              className="mt-2 pb-32"
              scrollEnabled={false}
              renderItem={({ item }) => (
                <PostCard {...item}/>
              )}
              />
          </>
        </View>
          }
        
      </ScrollView>
    </View>
  );
}
