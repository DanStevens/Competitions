#include <iostream>
#include <fstream>
#include <vector>
#include <unordered_set>
#include <tuple>

class CompoundWordsSolver {
private:
    std::unordered_set<std::string> wordsSet;
    std::vector<std::string> wordsSorted;
public:
    CompoundWordsSolver(const std::vector<std::string>& words) {
        wordsSorted = words;
        std::sort(wordsSorted.begin(), wordsSorted.end());
        wordsSet = std::unordered_set<std::string>(wordsSorted.begin(), wordsSorted.end());
    }
    std::vector<std::string> Solve() {
        std::vector<std::string> result;
        for (const std::string& word : wordsSorted) {
            for (const std::tuple<std::string, std::string>& tuple : GenerateDecompositions(word)) {
                if (wordsSet.count(std::get<0>(tuple)) && word.find(std::get<1>(tuple)) != std::string::npos) {
                    result.push_back(word);
                    break;
                }
            }
        }
        return result;
    }
    static std::vector<std::tuple<std::string, std::string>> GenerateDecompositions(const std::string& word) {
        std::vector<std::tuple<std::string, std::string>> result;
        for (int splitAt = 1; splitAt < word.length(); splitAt++) {
            result.push_back(std::make_tuple(word.substr(0, splitAt), word.substr(splitAt)));
        }
        return result;
    }
};

int main() {
    std::ifstream inputFile("input.txt");
    std::vector<std::string> words;
    std::string line;
    while (std::getline(inputFile, line)) {
        words.push_back(line);
    }
    inputFile.close();
    CompoundWordsSolver solver(words);
    std::vector<std::string> solution = solver.Solve();
    for (const std::string& word : solution) {
        std::cout << word << std::endl;
    }
    return 0;
}

